using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Resources;
using Resources.Models;
using WatchFace.Parser;
using WatchFace.Parser.Models;
using WatchFace.Parser.Utils;
using Image = System.Drawing.Image;
using Reader = WatchFace.Parser.Reader;
using Writer = WatchFace.Parser.Writer;
using NLog;
using NLog.Config;
using NLog.Targets;
using Newtonsoft.Json;
using BumpKit;
using System.Reflection;
using System.Drawing.Imaging;

namespace WatchFace_PackUnpack
{

    public partial class Form1 : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        PROGRAM_SETTINGS Program_Settings;
        bool Load_Settings = false;

        public Form1()
        {
            InitializeComponent();

            Program_Settings = new PROGRAM_SETTINGS();
            try
            {
                if (File.Exists(Application.StartupPath + @"\Settings.json"))
                {
                    Program_Settings = JsonConvert.DeserializeObject<PROGRAM_SETTINGS>
                                (File.ReadAllText(Application.StartupPath + @"\Settings.json"), new JsonSerializerSettings
                                {
                                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                                    NullValueHandling = NullValueHandling.Ignore
                                });
                }
                
            }
            catch (Exception)
            {
                //Logger.WriteLine("Ошибка чтения настроек " + ex);
            }
        }

        private void button_unpack_Click(object sender, EventArgs e)
        {
            // если копируем в папку
            if (checkBox_Watchface_Path.Checked)
            {
                string subPath = Application.StartupPath + @"\Watch_face\";
                if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);
                string respackerPath = Application.StartupPath + @"\Res_PackerUnpacker\";
                if (Is64Bit()) respackerPath = respackerPath + @"x64\resunpacker.exe";
                else respackerPath = respackerPath + @"x86\resunpacker.exe";

                OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "Json files (*.json) | *.json";
                openFileDialog.Filter = "Binary File (*.bin)|*.bin";
                ////openFileDialog1.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Путь к файлу циферблата";

                if (!File.Exists(respackerPath))
                {
                    MessageBox.Show("Путь [" + respackerPath +
                        "] к утилите распаковки/запаковки указан неверно.\r\n\r\n" +
                        "Отсутствуют необходимые компоненты программы.",
                        "Файл не найден", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fullfilename = openFileDialog.FileName;
                    string filename = Path.GetFileName(fullfilename);
                    string fullPath = subPath + filename;
                    // если файл существует
                    if (File.Exists(fullPath))
                    {
                        FormFileExists f = new FormFileExists();
                        f.ShowDialog();
                        int dialogResult = f.Data;

                        string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
                        string extension = Path.GetExtension(fullPath);
                        string path = Path.GetDirectoryName(fullPath);
                        string newFullPath = fullPath;
                        switch (dialogResult)
                        {
                            case 0:
                                return;
                            //break;
                            case 1:
                                File.Copy(fullfilename, fullPath, true);
                                newFullPath = Path.Combine(path, fileNameOnly);
                                if (Directory.Exists(newFullPath)) Directory.Delete(newFullPath, true);
                                break;
                            case 2:
                                int count = 1;

                                while (File.Exists(newFullPath))
                                {
                                    string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                                    newFullPath = Path.Combine(path, tempFileName + extension);
                                }
                                File.Copy(fullfilename, newFullPath);
                                fullPath = newFullPath;
                                fileNameOnly = Path.GetFileNameWithoutExtension(newFullPath);
                                path = Path.GetDirectoryName(newFullPath);
                                newFullPath = Path.Combine(path, fileNameOnly);
                                if (Directory.Exists(newFullPath)) Directory.Delete(newFullPath, true);
                                break;
                        }
                    }
                    else File.Copy(fullfilename, fullPath);

                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = respackerPath;
                        startInfo.Arguments = fullPath;
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();//ждем 
                        };
                        // этот блок закончится только после окончания работы программы 
                        //сюда писать команды после успешного завершения программы
                        string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
                        //string extension = Path.GetExtension(fullPath);
                        string path = Path.GetDirectoryName(fullPath);
                        //path = Path.Combine(path, fileNameOnly);
                        string newFullName_unp = Path.Combine(path, fileNameOnly + ".bin.unp");
                        //string newFullName_bin = Path.Combine(path, fileNameOnly + ".unp.bin");
                        string newFullName_bin = Path.Combine(path, fileNameOnly + ".bin");

                        //MessageBox.Show(newFullName);
                        if (File.Exists(newFullName_unp))
                        {
                            File.Copy(newFullName_unp, newFullName_bin, true);
                            File.Delete(newFullName_unp);
                            //this.BringToFront();
                            //Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + newFullName_bin));
                            if (File.Exists(newFullName_bin))
                            {
                                UnpackWatchFace(newFullName_bin);
                            }
                        }
                    }
                    catch
                    {
                        // сюда писать команды при ошибке вызова 
                    }


                }
            }
            else
            {
                string subPath = Application.StartupPath + @"\Watch_face\";
                if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);
                string respackerPath = Application.StartupPath + @"\Res_PackerUnpacker\";
                if (Is64Bit()) respackerPath = respackerPath + @"x64\resunpacker.exe";
                else respackerPath = respackerPath + @"x86\resunpacker.exe";

                OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "Json files (*.json) | *.json";
                openFileDialog.Filter = "Binary File (*.bin)|*.bin";
                ////openFileDialog1.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Путь к файлу циферблата";

                if (!File.Exists(respackerPath))
                {
                    MessageBox.Show("Путь [" + respackerPath +
                        "] к утилите распаковки/запаковки указан неверно.\r\n\r\n" +
                        "Отсутствуют необходимые компоненты программы.",
                        "Файл не найден", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fullPath = openFileDialog.FileName;

                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = respackerPath;
                        startInfo.Arguments = fullPath;
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();//ждем 
                        };
                        // этот блок закончится только после окончания работы программы 
                        //сюда писать команды после успешного завершения программы
                        string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
                        //string extension = Path.GetExtension(fullPath);
                        string path = Path.GetDirectoryName(fullPath);
                        //path = Path.Combine(path, fileNameOnly);
                        string newFullName_unp = Path.Combine(path, fileNameOnly + ".bin.unp");
                        string newFullName_bin = Path.Combine(path, fileNameOnly + ".bin");

                        //MessageBox.Show(newFullName);
                        if (File.Exists(newFullName_unp))
                        {
                            File.Copy(newFullName_unp, newFullName_bin, true);
                            File.Delete(newFullName_unp);
                            //this.BringToFront();
                            //Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + newFullName_bin));
                            if (File.Exists(newFullName_bin))
                            {
                                UnpackWatchFace(newFullName_bin);
                            }
                        }
                    }
                    catch
                    {
                        // сюда писать команды при ошибке вызова 
                    }
                }
            }

        }

        private static void UnpackWatchFace(string inputFileName)
        {
           var outputDirectory = CreateOutputDirectory(inputFileName);
            var baseName = Path.GetFileNameWithoutExtension(inputFileName);
            SetupLogger(Path.Combine(outputDirectory, $"{baseName}.log"));

            var reader = ReadWatchFace(inputFileName);
            if (reader == null) return;

            Logger.Debug("Exporting resources to '{0}'", outputDirectory);
            var reDescriptor = new FileDescriptor { Resources = reader.Resources };
            new Extractor(reDescriptor).Extract(outputDirectory);

            var watchFace = ParseResources(reader);

            if (ErrorCount.errorCount > 0)
            {
                MessageBox.Show("При распаковке циферблата были допущены ошибки (" + 
                    ErrorCount.errorCount.ToString() + "):" + ErrorCount.errorStr);
                ErrorCount.errorCount = 0;
                ErrorCount.errorStr = "";
            }
            if (watchFace == null) return;

            //GeneratePreviews(reader.Parameters, reader.Images, outputDirectory, baseName);

            //Logger.Debug("Exporting resources to '{0}'", outputDirectory);
            //var reDescriptor = new FileDescriptor {Resources = reader.Resources};
            //new Extractor(reDescriptor).Extract(outputDirectory);
            ExportWatchFaceConfig(watchFace, Path.Combine(outputDirectory, $"{baseName}.json"));
            GeneratePreviews(reader.Parameters, reader.Images, outputDirectory, baseName);
        }

        private void button_pack_Click(object sender, EventArgs e)
        {
            string subPath = Application.StartupPath + @"\Watch_face\";
            if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);
            string respackerPath = Application.StartupPath + @"\Res_PackerUnpacker\";
            if (Is64Bit()) respackerPath = respackerPath + @"x64\respacker.exe";
            else respackerPath = respackerPath + @"x86\respacker.exe";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = subPath;
            openFileDialog.Filter = "Json files (*.json) | *.json";
            //openFileDialog.Filter = "Binary File (*.bin)|*.bin";
            ////openFileDialog1.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Путь к файлу настроек циферблата";

            if (!File.Exists(respackerPath))
            {
                MessageBox.Show("Путь [" + respackerPath +
                    "] к утилите распаковки/запаковки указан неверно.\r\n\r\n" +
                    "Отсутствуют необходимые компоненты программы.",
                    "Файл не найден", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullfilename = openFileDialog.FileName;
                try
                {
                    PackWatchFace(fullfilename);

                    string fileNameOnly = Path.GetFileNameWithoutExtension(fullfilename);
                    //string extension = Path.GetExtension(fullPath);
                    string path = Path.GetDirectoryName(fullfilename);
                    string newFullName = Path.Combine(path, fileNameOnly + "_packed.bin");
                    if (File.Exists(newFullName))
                    {
                        double fileSize = (GetFileSizeMB(new FileInfo(newFullName)));
                        if (fileSize > 1.95) MessageBox.Show("Размер несжатого файла превышает 1,95МБ.\r\n\r\n" + "Циферблат может не работать.",
                            "Большой размер файла", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = respackerPath;
                        startInfo.Arguments = newFullName;
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();//ждем 
                        };
                        // этот блок закончится только после окончания работы программы 
                        //сюда писать команды после успешного завершения программы
                        fileNameOnly = Path.GetFileNameWithoutExtension(newFullName);
                        //string extension = Path.GetExtension(fullPath);
                        path = Path.GetDirectoryName(newFullName);
                        //path = Path.Combine(path, fileNameOnly);
                        string newFullName_cmp = Path.Combine(path, fileNameOnly + ".bin.cmp");
                        string newFullName_bin = Path.Combine(path, fileNameOnly + "_zip.bin");
                        if (File.Exists(newFullName_cmp))
                        {
                            File.Copy(newFullName_cmp, newFullName_bin, true);
                            File.Delete(newFullName_cmp);
                            Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + newFullName_bin));
                        }

                    }
                }
                catch
                {
                    // сюда писать команды при ошибке вызова 
                }
            }
        }

        private static void PackWatchFace(string inputFileName)
        {
            var baseName = Path.GetFileNameWithoutExtension(inputFileName);
            var outputDirectory = Path.GetDirectoryName(inputFileName);
            var outputFileName = Path.Combine(outputDirectory, baseName + "_packed.bin");
            SetupLogger(Path.ChangeExtension(outputFileName, ".log"));

            var watchFace = ReadWatchFaceConfig(inputFileName);
            if (watchFace == null) return;

            var imagesDirectory = Path.GetDirectoryName(inputFileName);
            try
            {
                WriteWatchFace(outputDirectory, outputFileName, imagesDirectory, watchFace);
            }
            catch (Exception)
            {
                File.Delete(outputFileName);
                throw;
            }
        }

        // проверка разрядности системы
        public static bool Is64Bit()
        {
            if (Environment.Is64BitOperatingSystem) return true;
            else return false;
        }

        public double GetFileSizeMB(FileInfo file)
        {
            try
            {
                double sizeinbytes = file.Length;
                double sizeinkbytes = Math.Round((sizeinbytes / 1024), 2);
                double sizeinmbytes = Math.Round((sizeinkbytes / 1024), 2);
                double sizeingbytes = Math.Round((sizeinmbytes / 1024), 2);
                return sizeinmbytes;
            }
            catch { return 0; } //перехват ошибок и возврат сообщения об ошибке
        }

        private static void SetupLogger(string logFileName)
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget
            {
                FileName = logFileName,
                Layout = "${level}|${message}",
                KeepFileOpen = true,
                ConcurrentWrites = false,
                OpenFileCacheTimeout = 30
            };
            config.AddTarget("file", fileTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));

            var consoleTarget = new ColoredConsoleTarget { Layout = @"${message}" };
            config.AddTarget("console", consoleTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));

            LogManager.Configuration = config;
        }

        private static string CreateOutputDirectory(string originalFileName)
        {
            var path = Path.GetDirectoryName(originalFileName);
            var name = Path.GetFileNameWithoutExtension(originalFileName);
            var unpackedPath = Path.Combine(path, $"{name}");
            if (!Directory.Exists(unpackedPath)) Directory.CreateDirectory(unpackedPath);
            return unpackedPath;
        }

        private static Reader ReadWatchFace(string inputFileName)
        {
            Logger.Debug("Opening watch face '{0}'", inputFileName);
            try
            {
                using (var fileStream = File.OpenRead(inputFileName))
                {
                    var reader = new Reader(fileStream);
                    Logger.Debug("Reading parameters...");
                    reader.Read();
                    return reader;
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                return null;
            }
        }

        private static WatchFace.Parser.WatchFace ParseResources(Reader reader)
        {
            Logger.Debug("Parsing parameters...");
            try
            {
                return ParametersConverter.Parse<WatchFace.Parser.WatchFace>(reader.Parameters);
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                return null;
            }
        }

        private static void ExportWatchFaceConfig(WatchFace.Parser.WatchFace watchFace, string jsonFileName)
        {
            Logger.Debug("Exporting config...");
            try
            {
                using (var fileStream = File.OpenWrite(jsonFileName))
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(JsonConvert.SerializeObject(watchFace, Formatting.Indented,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                    writer.Flush();
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
            }
        }

        private static void GeneratePreviews(List<Parameter> parameters, Bitmap[] images, string outputDirectory, string baseName)
        {
            Logger.Debug("Generating previews...");

            var states = GetPreviewStates();
            var staticPreview = PreviewGenerator.CreateImage(parameters, images, new WatchState());
            staticPreview.Save(Path.Combine(outputDirectory, $"{baseName}_static.png"), ImageFormat.Png);

            var previewImages = PreviewGenerator.CreateAnimation(parameters, images, states,
                CenterOffset.X, CenterOffset.Y, Model.modelByte);

            using (var gifOutput = File.OpenWrite(Path.Combine(outputDirectory, $"{baseName}_animated.gif")))
            using (var encoder = new GifEncoder(gifOutput))
            {
                foreach (var previewImage in previewImages)
                    encoder.AddFrame(previewImage, frameDelay: TimeSpan.FromSeconds(1));
            }
        }

        private static IEnumerable<WatchState> GetPreviewStates()
        {
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var previewStatesPath = Path.Combine(appPath, "PreviewStates.json");

            if (File.Exists(previewStatesPath))
                using (var stream = File.OpenRead(previewStatesPath))
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<WatchState>>(json);
                }

            var previewStates = GenerateSampleStates();
            using (var stream = File.OpenWrite(previewStatesPath))
            using (var writer = new StreamWriter(stream))
            {
                var json = JsonConvert.SerializeObject(previewStates, Formatting.Indented);
                writer.Write(json);
                writer.Flush();
            }

            return previewStates;
        }

        private static IEnumerable<WatchState> GenerateSampleStates()
        {
            var time = DateTime.Now;
            var states = new List<WatchState>();

            for (var i = 0; i < 10; i++)
            {
                var num = i + 1;
                var watchState = new WatchState
                {
                    BatteryLevel = 100 - i * 10,
                    Pulse = 60 + num * 2,
                    Steps = num * 1000,
                    Calories = num * 75,
                    Distance = num * 700,
                    Bluetooth = num > 1 && num < 6,
                    Unlocked = num > 2 && num < 7,
                    Alarm = num > 3 && num < 8,
                    DoNotDisturb = num > 4 && num < 9,

                    DayTemperature = -15 + 2 * i,
                    NightTemperature = -24 + i * 4,
                };

                if (num < 3)
                {
                    watchState.AirQuality = AirCondition.Unknown;
                    watchState.AirQualityIndex = null;

                    watchState.CurrentWeather = WeatherCondition.Unknown;
                    watchState.CurrentTemperature = null;
                }
                else
                {
                    var index = num - 2;
                    watchState.AirQuality = (AirCondition)index;
                    watchState.CurrentWeather = (WeatherCondition)index;

                    watchState.AirQualityIndex = index * 50 - 25;
                    watchState.CurrentTemperature = -10 + i * 6;
                }

                watchState.Time = new DateTime(time.Year, num, num * 2 + 5, i * 2, i * 6, i);
                states.Add(watchState);
            }

            return states;
        }

        private static WatchFace.Parser.WatchFace ReadWatchFaceConfig(string jsonFileName)
        {
            Logger.Debug("Reading config...");
            try
            {
                using (var fileStream = File.OpenRead(jsonFileName))
                using (var reader = new StreamReader(fileStream))
                {
                    return JsonConvert.DeserializeObject<WatchFace.Parser.WatchFace>(reader.ReadToEnd(),
                        new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Error,
                            NullValueHandling = NullValueHandling.Ignore
                        });
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                return null;
            }
        }

        private static void WriteWatchFace(string outputDirectory, string outputFileName, string imagesDirectory, WatchFace.Parser.WatchFace watchFace)
        {
            try
            {
                Logger.Debug("Reading referenced images from '{0}'", imagesDirectory);
                var imagesReader = new ResourcesLoader(imagesDirectory);
                imagesReader.Process(watchFace);

                Logger.Trace("Building parameters for watch face...");
                var descriptor = ParametersConverter.Build(watchFace);

                var baseFilename = Path.GetFileNameWithoutExtension(outputFileName);
                GeneratePreviews(descriptor, imagesReader.Images, outputDirectory, baseFilename);

                Logger.Debug("Writing watch face to '{0}'", outputFileName);
                using (var fileStream = File.OpenWrite(outputFileName))
                {
                    var writer = new Writer(fileStream, imagesReader.Resources, ColorType.colorType);
                    writer.Write(descriptor);
                    fileStream.Flush();
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                File.Delete(outputFileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Settings = true;
            if (Program_Settings.Model_GTS)
            {
                radioButton_gts.Checked = Program_Settings.Model_GTS;
            }
            else if (Program_Settings.Model_GTR42)
            {
                radioButton_gtr42.Checked = Program_Settings.Model_GTR42;
            }
            else
            {
                radioButton_gtr47.Checked = Program_Settings.Model_GTR47;
            }

            if (Program_Settings.Color1) radioButton_color1.Checked = true;
            else radioButton_color2.Checked = true;

            checkBox_Watchface_Path.Checked = Program_Settings.CopyInWatchFace;
            Load_Settings = false;
            Apply_Settings();
        }

        private void Save_Settings()
        {
            if (Load_Settings) return;
            Program_Settings.Model_GTR47 = radioButton_gtr47.Checked;
            Program_Settings.Model_GTR42 = radioButton_gtr42.Checked;
            Program_Settings.Model_GTS = radioButton_gts.Checked;

            Program_Settings.Color1 = radioButton_color1.Checked;
            Program_Settings.Color2 = radioButton_color2.Checked;

            Program_Settings.CopyInWatchFace = checkBox_Watchface_Path.Checked;
            string JSON_String = JsonConvert.SerializeObject(Program_Settings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Settings.json", JSON_String, Encoding.UTF8);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Save_Settings();
            Apply_Settings();
        }

        private void Apply_Settings()
        {
            if (Program_Settings.Model_GTR47)
            {
                CenterOffset.X = 227;
                CenterOffset.Y = 227;

                Model.modelByte = new byte[8] { 0x28, 0x00, 0x8c, 0xea, 0x00, 0x00, 0x01, 0xbc }; // gtr 47
            }
            else if (Program_Settings.Model_GTR42)
            {
                CenterOffset.X = 195;
                CenterOffset.Y = 195;

                Model.modelByte = new byte[8] { 0x2a, 0x00, 0x72, 0xeb, 0x00, 0x00, 0x5c, 0xd3 }; // gtr 47
            }
            else if (Program_Settings.Model_GTS)
            {
                CenterOffset.X = 174;
                CenterOffset.Y = 221;

                Model.modelByte = new byte[8] { 0x2e, 0x00, 0xaa, 0xeb, 0x00, 0x00, 0x68, 0xf1 }; // gtr 47
            }

            if (Program_Settings.Color1) ColorType.colorType = 1;
            else ColorType.colorType = 2;
        }
    }



    public static class CenterOffset
    {
        public static int X = 227;
        public static int Y = 227;
    }

    public static class Model
    {
        public static byte[] modelByte = new byte[8] { 0x28, 0x00, 0x8c, 0xea, 0x00, 0x00, 0x01, 0xbc }; // gtr 47
    }

    public static class ColorType
    {
        public static int colorType = 1;
    }

    class PROGRAM_SETTINGS
    {
        public bool Model_GTR47 = true;
        public bool Model_GTR42 = false;
        public bool Model_GTS = false;

        public bool Color1 = true;
        public bool Color2 = false;
        public bool CopyInWatchFace = true;
    }
}
