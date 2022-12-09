using CommunityToolkit.Maui.Core.Extensions;
using System.IO.Ports;

namespace AudioVisual.Services
{
    public class ArduinoService
    {
        private SerialPort _serialPort;

        public ArduinoService(string port = "COM3")
        {
            // SERIAL_8N1 
            _serialPort = new SerialPort(port, 2000000, Parity.None, 8, StopBits.One);
            // _serialPort.WriteBufferSize = 512;

        }

        public void SendLightData(List<Color> colors)
        {

            var colorsBytes = colors.SelectMany(c => new byte[] {
                 c.GetByteGreen() >= 255 ? (byte)254 : c.GetByteGreen(),
                c.GetByteRed() >= 255 ? (byte)254 : c.GetByteRed(),
                c.GetByteBlue() >= 255 ? (byte)254 : c.GetByteBlue()
            }
            ).ToArray();
            _serialPort.Open();
            /*foreach (var c in colors)
            {
                var cb = new byte[] { c.GetByteRed(), c.GetByteGreen(), c.GetByteBlue() };
                _serialPort.Write(cb, 0, cb.Length);
    
            }*/
            // string data = BitConverter.ToString(colorsBytes);
            byte[] data = colorsBytes.Concat(new List<byte>() { 255, 255, 255, 255 }).ToArray();
            _serialPort.Write(data, 0, data.Length);
            //_serialPort.WriteLine("");
            _serialPort.Close();
        }
    }
}
