using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoControllerComm.Adapter.Communication;
using IoControllerComm.Adapter.DataTypes;
using IoControllerComm.Adapter.Responses;
using IoControllerComm.Service;
using CroptimalLabSW.Model.DB;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaManage
    {
        private static ChromaManage s_chromaManage = null;
        private static object s_LockChromaManage = new object();
        internal IIoService m_ioService = new IoService();
        private DBOptions m_DBOptions;

        private bool _connectionOn;
        private int _commPort;
        private int _keepAliveSec;
        private bool[] m_maskPorts;
        private bool[] m_statePorts;
        private int m_quantityOfLEDs;

        public int KeepAliveSec
        {
            get { return _keepAliveSec; }
            set { _keepAliveSec = value; }
        }
        public int CommPort
        {
            get { return _commPort; }
            set { _commPort = value; }
        }
        public bool ConnectionOn
        {
            get { return _connectionOn; }
            set { _connectionOn = value; }
        }

        public ChromaManage()
        {
            m_DBOptions = DBOptions.Instance();
            m_quantityOfLEDs = m_DBOptions.getChromameterQuantityOfLEDs();
            m_maskPorts = new bool[m_quantityOfLEDs];
            m_maskPorts = Enumerable.Repeat(false, m_maskPorts.Length).ToArray();
            m_statePorts = new bool[m_quantityOfLEDs];
            initPortsState();
        }

        private void initPortsState()
        {
            m_statePorts = Enumerable.Repeat(false, m_statePorts.Length).ToArray();
        }

        public static ChromaManage Instance()
        {
            lock (s_LockChromaManage)
            {
                if (s_chromaManage == null)
                {
                    s_chromaManage = new ChromaManage();
                }
            }
            return s_chromaManage;
        }

        public bool openConnection()
        {
            InitDataDto data = new InitDataDto()
            {
                KeepAlive = (UInt32)KeepAliveSec,
                Port = CommPort,
            };
            GeneralResponse resp = m_ioService.IoAdapter.Init(data);
            if(resp.ResponseError == CommErrors.NoError)
            {
                ConnectionOn = true;
                return true;
            }
            return false;
        }

        public bool closeConnection()
        {
            GeneralResponse resp = m_ioService.IoAdapter.Close();
            if (resp.ResponseError == CommErrors.NoError)
            {
                ConnectionOn = false;
                return true;
            }
            return false;
        }

        public bool setMaskLEDs(bool[] i_portsState)
        {
            GeneralResponse resp = m_ioService.IoAdapter.SetMaskIoPorts(new SetMaskPortDto(m_maskPorts, i_portsState));
            if (resp.ResponseError == CommErrors.NoError)
            {
                Array.Copy(i_portsState, m_statePorts, i_portsState.Length);
                return true;
            }
            return false;
        }
        
        public bool setLED(int i_LEDNum, OnOffEnum i_onOff)
        {
            SetIoPortDto data = new SetIoPortDto();
            data.Port = (byte)i_LEDNum;
            data.State = i_onOff;
            GeneralResponse resp = m_ioService.IoAdapter.SetIoPort(data);
            if (resp.ResponseError == CommErrors.NoError)
            {
                return true;
            }
            return false;
        }

        public bool GetAnalogInput(int i_channel, ref float io_result)
        {
            var resp = m_ioService.IoAdapter.GetAnalogInput(i_channel);
            if (resp.ResponseError == CommErrors.NoError)
            {
                io_result = resp.GetAnalogInputDto.Value;
                return true;
            }
            return false;
        }

        public bool GetAllAnalogInput(ref float[] io_resultsArr)
        {
            var resp = m_ioService.IoAdapter.GetAllAnalogInputs();
            if (resp.ResponseError == CommErrors.NoError)
            {
                io_resultsArr = resp.GetAllAnalogInputsDto.Values;
                return true;
            }
            return false;
        }
    }
}
