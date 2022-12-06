using CommunityToolkit.Maui.Core.Extensions;
using System.IO.Ports;

namespace AudioVisual.Services
{
    public class ArduinoService
    {
        private SerialPort _serialPort;

        public ArduinoService(string port = "COM3")
        {
            _serialPort = new SerialPort(port, 2000000, Parity.Odd, 8, StopBits.One);
        }

        public void SendLightData(List<Color> colors)
        {
            _serialPort.Open();
            var colorsBytes = colors.SelectMany(c => new byte[] { c.GetByteRed(), c.GetByteGreen(), c.GetByteBlue() }).ToList();
            _serialPort.Write(colorsBytes.ToArray(), 0, colorsBytes.Count);
            _serialPort.Close();
        }
    }
}
