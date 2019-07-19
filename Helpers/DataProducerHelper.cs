using HC.Device.Bike.Codec;
using LinnerToolkit.Core.IO;
using LinnerToolkit.Core.IO.Serial;
using LinnerToolkit.Desktop.IO;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace RidingSystem.Helpers
{
    public class DataProducerHelper
    {
        public IDataProducer DataProducer { get; protected set; }

        protected static DataProducerHelper _current;
        public static DataProducerHelper Current
        {
            get
            {
                if (_current == null)
                    _current = new DataProducerHelper();

                return _current;
            }
        }

        protected DataProducerHelper()
        {
            string portName = ConfigurationManager.AppSettings["PortName"];
            int baudRate;
            if (!string.IsNullOrEmpty(portName) && int.TryParse(ConfigurationManager.AppSettings["BaudRate"], out baudRate))
            {
                DataProducer = new SerialDataProducer(new SerialEndPoint(portName, baudRate), new DataDecoder()) { MaxReconnectTime=int.MaxValue};
                //DataProducer.Opened += DataProducer_Opened;
                //DataProducer.Closed += DataProducer_Closed;
                //DataProducer.ExceptionCaught += DataProducer_ExceptionCaught;

                Task.Run(async () => 
                {
                    while (!await DataProducer.Open())
                    {
                        await Task.Delay(1000);
                       // System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                });


            }
        }

        private void DataProducer_ExceptionCaught(object sender, IoExceptionEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("StackTrace: " + e.Exception.StackTrace + ", Message: " + e.Exception.Message);
            }));
        }

        private void DataProducer_Closed(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("串口已关闭");
            }));
        }

        private void DataProducer_Opened(object sender, System.EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("串口已打开");
            }));
        }
    }
}
