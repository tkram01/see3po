using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
/* Created by Debarati-- This class can work with both .jpg and .bmp files,
 * other image types has not been tested yet.
 * we need to provide it with the full location of the image file for m_myImage property and block length for 
 * the array to be generated */
public class ImageToArray
{
    private Bitmap m_myImage;
    private int m_blockLength;

    public ImageToArray(Bitmap myimage, int blocklength)
    {
        this.m_myImage = myimage;
        this.m_blockLength = blocklength;
    }
    public int[,] createArray()
    {
        int num_height = this.m_myImage.Height;
        int num_width = this.m_myImage.Width;
        int blockLength = this.m_blockLength;

        int[,] arr = new int[num_height / blockLength, num_width / blockLength];
        return arr;
    }
/*  // this part is not necessary, was created for testing purpose only, it could be removed
    static void Main(string[] args)
    {
        if (args.Length < 1)
            return;
        //arr will represent the original image in 2D array form.
        ImageToArray image = new ImageToArray(new Bitmap(args[0]), 5);

        int[,] arr = image.createArray();
        arr = image.getBlockArray(image);
       
        // change it to args[1] later for commandline execution and also change the same
        //in printArray(int[,],String path)....
        image.printArray(arr, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\created.txt");
     //   image.printArray(arr_small, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\derived.txt");

    } */
    public int[,] getBlockArray(ImageToArray image)
    {
        //height and width of the original image file
        int num_width = image.m_myImage.Width;
        int num_height = image.m_myImage.Height;


        // height and width of the array representation of the new array to be created
        // each cell represent the walkable or non-walkable block defined earlier

        int workingHeight = num_height / m_blockLength;
        int workingWidth = num_width / m_blockLength;

        // new array to hold information of the image
        int[,] imageArray = new int[workingHeight, workingWidth];

        //any color darker than gray is non walkable
        Color gray = Color.Gray;
        for (int row = 0; row < workingHeight * m_blockLength; row++)
        {
            for (int col = 0; col < workingWidth * m_blockLength; col++)
            {
                Color pixelColor = image.m_myImage.GetPixel(col, row);
                if (pixelColor.R < gray.R && pixelColor.G < gray.G && pixelColor.B < gray.B)
                {
                    imageArray[row / m_blockLength, col / m_blockLength] = 1;
                }
            }
        }
        return imageArray;
    }
    // prints the 2D array generated in a file.
    public void printArray(int[,] arr, String path)
    {
        // for now need to modify the location here manually
       
        using (TextWriter tw = new StreamWriter(path))
        {
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    tw.Write(arr[row, col]);
                }
                tw.WriteLine();
            }
        }
    }

}
