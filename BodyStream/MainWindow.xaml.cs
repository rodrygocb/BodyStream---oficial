using System;
using Microsoft.Kinect;

class Program
{
    static void Main(string[] args)
    {
        // Inicialize o sensor Kinect
        var kinectSensor = KinectSensor.GetDefault();

        if (kinectSensor != null)
        {
            // Abra o sensor
            kinectSensor.Open();

            // Crie um leitor de infravermelho
            var infraredFrameDescription = kinectSensor.InfraredFrameSource.FrameDescription;
            var infraredFrameReader = kinectSensor.InfraredFrameSource.OpenReader();

            // Configurar o manipulador de evento para capturar quadros infravermelhos
            infraredFrameReader.FrameArrived += (sender, eventArgs) =>
            {
                using (var infraredFrame = eventArgs.FrameReference.AcquireFrame())
                {
                    if (infraredFrame != null)
                    {
                        // Obter os dados de infravermelho
                        ushort[] infraredData = new ushort[infraredFrameDescription.Width * infraredFrameDescription.Height];
                        infraredFrame.CopyFrameDataToArray(infraredData);

                        // Processar os dados de infravermelho
                        // Aqui você pode implementar sua própria lógica para processar os dados

                        // Exemplo: exibir um valor de pixel no meio da tela
                        int centerX = infraredFrameDescription.Width / 2;
                        int centerY = infraredFrameDescription.Height / 2;
                        int index = centerX + centerY * infraredFrameDescription.Width;
                        ushort infraredValue = infraredData[index];

                        Console.WriteLine($"Valor do pixel no centro da tela: {infraredValue}");
                    }
                }
            };

            Console.WriteLine("Capturando dados de infravermelho. Pressione qualquer tecla para sair.");
            Console.ReadKey();

            // Fechar o leitor de infravermelho
            infraredFrameReader.Dispose();

            // Fechar o sensor Kinect
            kinectSensor.Close();
        }
        else
        {
            Console.WriteLine("Nenhum sensor Kinect encontrado.");
        }
    }
}
