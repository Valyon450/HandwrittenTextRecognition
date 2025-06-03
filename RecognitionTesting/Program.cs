using System.Text;
using Tesseract;

string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
string tessDataPath = Path.Combine(projectDirectory, @"..\..\..\tessdata\");

string language = "ukr";

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("-------------------ТЕСТУВАННЯ РОЗПІЗНАВАННЯ ДРУКОВАНОГО ТЕКСТУ---------------------------");

string imagePath = Path.Combine(projectDirectory, @"..\..\..\InputImages\ЗРАЗОК-1.jpg");

if (!File.Exists(imagePath))
{
    Console.WriteLine("Image file not found!");
    return;
}

try
{
    using (var engine = new TesseractEngine(tessDataPath, language, EngineMode.Default))
    {
        using (var img = Pix.LoadFromFile(imagePath))
        {
            using (var page = engine.Process(img))
            {
                string text = page.GetText();
                Console.WriteLine("Recognized text:");
                Console.WriteLine(text);
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine("-------------------ТЕСТУВАННЯ РОЗПІЗНАВАННЯ РУКОПИСНОГО ТЕКСТУ---------------------------");

imagePath = Path.Combine(projectDirectory, @"..\..\..\InputImages\115459841_00154.jpeg");

language = "rus";

if (!File.Exists(imagePath))
{
    Console.WriteLine("Image file not found!");
    return;
}

try
{
    using (var engine = new TesseractEngine(tessDataPath, language, EngineMode.Default))
    {
        using (var img = Pix.LoadFromFile(imagePath))
        {
            using (var page = engine.Process(img))
            {
                string text = page.GetText();
                Console.WriteLine("Recognized text:");
                Console.WriteLine(text);
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}