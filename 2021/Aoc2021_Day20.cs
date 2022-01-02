using System.Text;

public class Aoc2021_Day20 : BaseDay
{
    private char DarkPixel => '.';
    private char LightPixel => '#';
    private string ImageEnhancement { get; set; }

    private List<string> ImagePadded { get; set; } = new List<string>();

    public Aoc2021_Day20(string inputFileName) : base(inputFileName)
    {

    }

    public override void RunA()
    {
        ImageEnhancement = Input[0];
        var imageInput = Input.Skip(2).ToList();

        List<string> imageEnhanced = EnhanceImage(imageInput);
        List<string> imageEnhanced2 = EnhanceImage(imageEnhanced);
        
        var lightPixelCount = String.Join("", imageEnhanced).Count(x => x == '#');
        imageEnhanced.ToList();
    }

    private List<string> EnhanceImage(List<string> imageInput)
    {
        var paddedImage = PadImage(imageInput);

        var nrRows = paddedImage.Count;
        var nrCols = paddedImage[0].Length;

        int pixelVal = 0;
        List<string> imageEnhanced = new List<string>();

        imageEnhanced.Add(GetPadLine(imageInput));
        for (int row = 1; row < nrRows - 2; row++)
        {
            var rowBuilder = new StringBuilder();
            rowBuilder.Append(DarkPixel);
            for (int col = 1; col < nrRows - 2; col++)
            {
                pixelVal = GetPixelValue(paddedImage, row, col);
                var pixel = ImageEnhancement[pixelVal];
                rowBuilder.Append(pixel);
                if (row == 3 && col == 3)
                    System.Console.WriteLine($"Bernt {pixel}");
            }
            rowBuilder.Append(DarkPixel);
            imageEnhanced.Add(rowBuilder.ToString());
        }
        imageEnhanced.Add(GetPadLine(imageInput));
        return imageEnhanced;
    }

    private List<string> PadImage(List<string> imageToPad)
    {
        var paddedImage = new List<string>();
        string padLine = GetPadLine(imageToPad);

        paddedImage.Add(padLine);
        foreach (var line in imageToPad)
        {
            var paddedLine = DarkPixel + line + DarkPixel;
            paddedImage.Add(paddedLine);
        }
        paddedImage.Add(padLine);

        return paddedImage;
    }

    private string GetPadLine(List<string> imageToPad)
    {
        return new string(DarkPixel, imageToPad[0].Length + 2);
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

    public override void RunB()
    {

    }
}