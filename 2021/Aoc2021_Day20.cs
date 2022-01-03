using System.Text;

public class Aoc2021_Day20 : BaseDay
{
    private char DarkPixel => '.';
    private char LightPixel => '#';

    private int EnhanceNr { get; set; } = 0;
    private string ImageEnhancement { get; set; }

    private List<string> ImageInput { get; set; } = new List<string>();
    private List<string> ImagePadded { get; set; } = new List<string>();

    public Aoc2021_Day20(string inputFileName) : base(inputFileName)
    {
        ImageEnhancement = Input[0];
        ImageInput = Input.Skip(2).ToList();
    }

    public override void RunA()
    {
        EnhanceXTimes(2);
    }

    public override void RunB()
    {
        EnhanceXTimes(50);
    }

    private void EnhanceXTimes(int times)
    {
        List<string> imageEnhanced = EnhanceImageOnce(ImageInput);

        for (int i = 2; i <= times; i++)
        {
            imageEnhanced = EnhanceImageOnce(imageEnhanced);

            var lightPixelCount1 = String.Join("", imageEnhanced).Count(x => x == LightPixel);
            System.Console.WriteLine($"Number of lit pixels is {lightPixelCount1} after {i} enhancement");
        }
    }

    private List<string> EnhanceImageOnce(List<string> ImageInput)
    {
        EnhanceNr++;
        var paddedImage = PadImage(PadImage(PadImage(ImageInput)));

        var nrRows = paddedImage.Count;
        var nrCols = paddedImage[0].Length;

        int pixelVal = 0;
        List<string> imageEnhanced = new List<string>();

        for (int row = 1; row < nrRows - 1; row++)
        {
            var rowBuilder = new StringBuilder();
            for (int col = 1; col < nrCols - 1; col++)
            {
                pixelVal = GetPixelValue(paddedImage, row, col);
                var pixel = ImageEnhancement[pixelVal];
                rowBuilder.Append(pixel);
            }
            imageEnhanced.Add(rowBuilder.ToString());
        }
        return imageEnhanced;
    }

    private List<string> PadImage(List<string> imageToPad)
    {
        char padChar = DarkPixel;
        if (ImageEnhancement[0] == '#' && EnhanceNr % 2 == 0)
            padChar = LightPixel;
        var paddedImage = new List<string>();
        string padLine = GetPadLine(imageToPad[0].Length + 2);

        paddedImage.Add(padLine);
        foreach (var line in imageToPad)
        {
            var paddedLine = padChar + line + padChar;
            paddedImage.Add(paddedLine);
        }
        paddedImage.Add(padLine);

        return paddedImage;

        string GetPadLine(int length)
        {
            return new string(padChar, length);
        }
    }

    private int GetPixelValue(List<string> imagePadded, int row, int col)
    {
        var line1 = imagePadded[row - 1].Substring(col - 1, 3);
        var line2 = imagePadded[row].Substring(col - 1, 3);
        var line3 = imagePadded[row + 1].Substring(col - 1, 3);
        var pixelLine = line1 + line2 + line3;
        var binaryLine = pixelLine.Replace(DarkPixel, '0').Replace(LightPixel, '1');
        var decimalValue = Convert.ToInt32(binaryLine, 2);
        return decimalValue;
    }

}