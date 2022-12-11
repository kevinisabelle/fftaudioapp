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
            _serialPort = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
            // _serialPort.WriteBufferSize = 512;
            _serialPort.Open();
        }

        public void SendLightData(List<Color> colors)
        {

            var colorsBytes = colors.SelectMany(c => new byte[] {
                 c.GetByteGreen() >= 255 ? (byte)254 : c.GetByteGreen(),
                c.GetByteRed() >= 255 ? (byte)254 : c.GetByteRed(),
                c.GetByteBlue() >= 255 ? (byte)254 : c.GetByteBlue()
            }
            ).ToArray();

            /*foreach (var c in colors)
            {
                var cb = new byte[] { c.GetByteRed(), c.GetByteGreen(), c.GetByteBlue() };
                _serialPort.Write(cb, 0, cb.Length);
    
            }*/
            // string data = BitConverter.ToString(colorsBytes);
            byte[] data = colorsBytes.Concat(new List<byte>() { 0xFF }).ToArray();
            _serialPort.Write(data, 0, data.Length);
            //_serialPort.WriteLine("");
            // _serialPort.Close();
        }
    }
}
