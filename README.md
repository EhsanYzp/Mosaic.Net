# Mosaic.Net
A simple application for creating mosaic images using C# and .NET

This is a simple project in C# and .NET 5 for creating the mosaic images.

The first version is a console application and it does following:

1. Get Parameters from the User: 6 user-defined parameters for initiating the mosaic generation will be obtained from user:
        - Number of Mosaic Rows
        - Number of Mosaic Columns
        - Input Path of Images 
        - Output Path for Saving Mosaic Image
        - Input Images Extension
        - Mosaic Image Extension
The values should be specified correctly, as instructed in the console. Otherwise, the process will be failed. In the future versions, the necessary checks will be integrated into this function.

2. The input images will be processed and placed into memory for further processing.
In this step, the process reads all images with the specified extension (if any) from input specified directory and save them in a in-memory bitmap list. Height and width parameters of final mosaic will also be calculated as part of the logic. The current version throws some exception if something goes wrong during this process.

3. The final Mosaic Image will be created.
In this step, the process navigates through the list of saved in-memory bitmaps and save the corresponding bitmap image in the final mosaic bitmap. Once the navigation process finishes, the final bitmap will be saved in the output directory specified by the user and with the specified file extension. The current version throws some exception if something goes wrong during this process. 

Any suggestion/recommendation for improving this solution is welcome!

Thanks!
