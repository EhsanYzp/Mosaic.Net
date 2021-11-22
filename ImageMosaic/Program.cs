using System;
using System.Collections.Generic;
using System.IO;

namespace ImageMosaic
{
    class Program
    {
        private static string inputPath;            //Input Path of images
        private static string outputPath;           //Output path to save mosaic image

        private static int rowNumber;               //User defined number of rows for mosaic image
        private static int columnNumber;            //User defined number of columns for mosaic image


        private static string inputFilesExtension;  //Input images file extension for filtering
        private static string outputFileExtension;  //Mosaic image user defined extension for saving

        private static System.Drawing.Bitmap finalImage = null; //Bitmap instance for saving final mosaic image
        private static List<System.Drawing.Bitmap> images;      //List of bitmap images of input files
        private static System.Drawing.Bitmap bitmap;            //A bitmap instance of currently under process image

        private static int height;                              //height of a single image in pixels
        private static int width;                               //width of a single image in pixels
        private static int totalWidth;                          //width of the mosaic image (height * columnNumber)
        private static int totalHeight;                         //height of the mosaic image (width * rowNumber)

        private static bool ifSuccessfull = false;              //a flag indicating errors during mosaic generation

        private static string[] files;                          //an array of strings containing the paths of input images


        //Entry Point
        static void Main(string[] args)
        {
            //Get Parameters from User
            Initialization();

            //Read Images from Input Directory into memory
            ifSuccessfull = ReadImagesIntoMemory();

            //Create Mosaic Image from Input Images loaded in memory
            ifSuccessfull = CreateMosaic();

            //Success or Failure Log into console.
            if (ifSuccessfull)
                Console.WriteLine("Mosaic Image Created Successfully.");
            else
                Console.WriteLine("An error happened while trying to create the Mosaic Image.");
        }

        /// <summary>
        /// This function gets 5 user-defined parameters for initiating the mosaic generation process:
        /// Number of Rows, Number of Columns, Input Path of Images, 
        /// Output Path for Saving Mosaic Image, Input Images Extension, Mosaic Image Extension
        /// The values should be specified correctly, as instructed in the console.
        /// otherwise, the process will be failed.
        /// In the future versions, the necessary checks will be integrated into this function.
        /// </summary>
        private static void Initialization()
        {
            Console.WriteLine("Please Specify Number of Rows (e.g: 10): ");
            rowNumber = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please Specify Number of Columns (e.g: 5): ");
            columnNumber = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please Specify Input Path of Images (e.g: C:\\In): ");
            inputPath = Console.ReadLine();

            Console.WriteLine("Please Specify Output Path for Saving Mosaic Image (e.g: C:\\Out): ");
            outputPath = Console.ReadLine();

            Console.WriteLine("Please Specify Input Images Extension (e.g: .png, .jpg): ");
            inputFilesExtension = Console.ReadLine();

            Console.WriteLine("Please Specify Output Mosaic Image Extension (e.g: .png, .jpg): ");
            outputFileExtension = Console.ReadLine();
        }


        /// <summary>
        /// This method reads all images with the specified extension (if any)
        /// from input specified directory, save them in a in-memory bitmap list
        /// height and width parameters of final mosaic will also be calculated as part of the logic.
        /// The current version throws some exception if something goes wrong during this process.
        /// </summary>
        /// <returns></returns>
        private static bool ReadImagesIntoMemory()
        {
            try
            {
                files = Directory.GetFiles(inputPath, $"*{inputFilesExtension}");

                //read all images into memory
                images = new List<System.Drawing.Bitmap>();

                bitmap = new System.Drawing.Bitmap(files[0]);

                height = bitmap.Height;
                width = bitmap.Width;

                totalWidth = width * columnNumber;
                totalHeight = height * rowNumber;

                foreach (var file in files)
                {
                    bitmap = new System.Drawing.Bitmap(file);
                    images.Add(bitmap);
                }
                finalImage = new System.Drawing.Bitmap(totalWidth, totalHeight);
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                return false;
            }
        }


        /// <summary>
        /// This method navigates through the list of saved in-memory bitmaps
        /// and save the corresponding bitmap image in the final mosaic bitmap.
        /// Once the navigation process finishes, the final bitmap will be saved 
        /// in the output directory specified by the user and with the specified file extension.
        /// The current version throws some exception if something goes wrong during this process. 
        /// </summary>
        /// <returns></returns>
        public static bool CreateMosaic()
        {
            try
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {

                    g.Clear(System.Drawing.Color.White);

                    int nthImage = 0;
                    for (int row = 0; row < rowNumber; row++)
                    {
                        for (int column = 0; column < columnNumber; column++)
                        {
                            nthImage = row * columnNumber + column;
                            g.DrawImage(images[nthImage], new System.Drawing.Rectangle(column * height, row * width, width, height));
                        }
                    }
                }
                finalImage.Save($"{outputPath}\\mosaic{DateTime.Now.ToString("yyyy-dd-MM--HH-mm-ss")}{outputFileExtension}");
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                return false;
            }
            return true;
        }
    }
}
