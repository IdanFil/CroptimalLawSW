//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IoControllerComm.Adapter.Communication;
//using IoControllerComm.Adapter.DataTypes;
//using IoControllerComm.Adapter.Responses;
//using IoControllerComm.Service;

//namespace CroptimalLabSW.Model.Chromameter
//{
//    class ChromaManage
//    {
//        internal IIoService m_ioService = new IoService();


//        private void SetIoPort(int index)
//        {
//            StartRunningAction();
//            SetIoPortDto data = new SetIoPortDto()
//            {
//                Port = (byte)index,
//                State = (m_setPorts[index] == true) ? OnOffEnum.On : OnOffEnum.Off
//            };
//            m_ioService.SetIoPort(data, (p) =>
//            {
//                var resp = p.ResponseError;
//                EndRunningAction(p.ResponseError);
//            });
//        }

//        private void InitController(int i_keepAliveSec, int i_port)
//        {
//            StartRunningAction();
//            InitDataDto data = new InitDataDto()
//            {
//                KeepAlive = (UInt32)i_keepAliveSec,
//                Port = i_port,
//            };
//            data.Port = i_port;
//            m_ioService.InitController(data, (p) =>
//            {
//                var resp = p.ResponseError;
//                if (resp == CommErrors.NoError)
//                {
//                    InitPorts();
//                }
//                EndRunningAction(p.ResponseError);
//            });
//        }

//        private void SetMaskIoPorts(object parameter)
//        {
//            StartRunningAction();
//            m_ioService.SetMaskIoPorts(new SetMaskPortDto(m_maskPorts, m_statePorts), (p) =>
//            {
//                EndRunningAction(p.ResponseError);
//            });
//        }

//        private void GetSwVersion(object parameter)
//        {
//            StartRunningAction();
//            Version = "";
//            m_ioService.GetSwVersion((p) =>
//            {
//                GetSwVersionResponse swVer = p as GetSwVersionResponse;
//                if ((swVer != null) && (swVer.ResponseError == CommErrors.NoError))
//                {
//                    Version = swVer.SwVersionDataDto.ToString();
//                }
//                EndRunningAction(p.ResponseError);
//            });

//        }

//        private void GetAnalogInput(object parameter)
//        {
//            StartRunningAction();
//            AnalogInputValue = null;
//            m_ioService.GetAnalogInput(SelectedChannel, (p) =>
//            {
//                GetAnalogInputResponse anIn = p as GetAnalogInputResponse;
//                if ((anIn != null) && (anIn.ResponseError == CommErrors.NoError))
//                {
//                    AnalogInputValue = anIn.GetAnalogInputDto.Value;
//                }
//                EndRunningAction(p.ResponseError);
//            });
//        }

//        private void GetAllAnalogInput(object parameter)
//        {
//            ClearAnalogInputs();
//            StartRunningAction();
//            m_ioService.GetAllAnalogInput((p) =>
//            {
//                GetAllAnalogInputsResponse anIn = p as GetAllAnalogInputsResponse;
//                if ((anIn != null) && (anIn.ResponseError == CommErrors.NoError))
//                {
//                    AnalogInputValues = anIn.GetAllAnalogInputsDto.Values;
//                }
//                EndRunningAction(p.ResponseError);
//            });
//        }

//        private void StartRunningAction()
//        {
//            EnableActions = false;
//            ClearError();
//        }

//        private void EndRunningAction(CommErrors err)
//        {
//            EnableActions = true;
//            Error = err.ToString();
//        }

//        private void InitPorts()
//        {
//            m_internalChange = true;
//            Port0 = false;
//            Port1 = false;
//            Port2 = false;
//            Port3 = false;
//            Port4 = false;
//            Port5 = false;
//            Port6 = false;
//            Port7 = false;
//            m_internalChange = false;

//            for (int i = 0; i < MaskPorts.Length; i++)
//            {
//                MaskPorts[i] = false;
//            }
//            for (int i = 0; i < StatePorts.Length; i++)
//            {
//                StatePorts[i] = false;
//            }
//            RaisePropertyChanged(() => MaskPorts);
//            RaisePropertyChanged(() => StatePorts);
//        }

//        private void ClearError()
//        {
//            Error = "";
//        }

//        private void ClearAnalogInputs()
//        {
//            AnalogInputValues = null;
//        }

//        private void OnAnalogInputTimer(object sender, ElapsedEventArgs e)
//        {
//            var resp = m_ioService.IoAdapter.GetAnalogInput(SelectedChannel);
//            if (resp.ResponseError == CommErrors.NoError)
//            {
//                AnalogInputValue = resp.GetAnalogInputDto.Value;
//            }
//            else
//            {
//                if (AiPollingStopError == true)
//                {
//                    AnalogInputPolling = false;
//                }
//            }
//            Error = resp.ResponseError.ToString();
//        }

//        private void OnAllAnalogInputTimer(object sender, ElapsedEventArgs e)
//        {
//            var resp = m_ioService.IoAdapter.GetAllAnalogInputs();
//            if (resp.ResponseError == CommErrors.NoError)
//            {
//                AnalogInputValues = resp.GetAllAnalogInputsDto.Values;
//            }
//            else
//            {
//                if (AllAiPollingStopError == true)
//                {
//                    AllAnalogInputPolling = false;
//                }
//            }
//            Error = resp.ResponseError.ToString();
//        }
//    }
//}
