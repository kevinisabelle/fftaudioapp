using CommunityToolkit.Maui.Core.Extensions;
using System.IO.Ports;

namespace AudioVisual.Services
{
    public class ArduinoService
    {
        private SerialPort _serialPort;

        public ArduinoService(string port)
        {
            // SERIAL_8N1 
            _serialPort = new SerialPort(port, 500000, Parity.None, 8, StopBits.One);
            _serialPort.WriteBufferSize = 480 * 4;
        }

        public void SendLightData(List<Color> colors)
        {
            try
            {
                _serialPort.Open();
                var colorsBytes = colors.SelectMany(c => new byte[] {
                 c.GetByteGreen() >= 255 ? (byte)254 : c.GetByteGreen(),
                c.GetByteRed() >= 255 ? (byte)254 : c.GetByteRed(),
                c.GetByteBlue() >= 255 ? (byte)254 : c.GetByteBlue()
            }
                ).ToArray();

                byte[] data = colorsBytes.Concat(new List<byte>() { 0xFF }).ToArray();
                _serialPort.Write(data, 0, data.Length);
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                // Ignore
            }
        }
    }
}
