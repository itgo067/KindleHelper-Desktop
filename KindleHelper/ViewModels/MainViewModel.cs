using KindleHelper.Models;
using KindleHelper.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KindleHelper.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            SelectFileTypeCommand = new RelayCommand<string>(SelectFileType);
            GetBookListCommand = new RelayCommand(GetBookList);
            DownBooksCommand = new RelayCommand(DownloadBooks);
            //SelectFileType("PDOC");
            initData();
            Books = new ObservableCollection<BookItem>();
            //CreateBooks();
        }

        private BackgroundWorker worker = new BackgroundWorker();

        private void DownloadBooks()
        {
            if (isInProgress)
            {
                return;
            }

            isInProgress = true;

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            isInProgress = false;
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            BackgroundWorker worker = sender as BackgroundWorker;


            var step = 100.0 / Books.Count;
            for (int i = 0; i < Books.Count; i++)
            {
                var book = Books[i];
                sb.AppendLine($"正在下载 - {book.Title}");
                Log = sb.ToString();
                Task.Run(async () =>
                {
                   await  HttpUtil.DownloadFile(kindleUrl, book.Asin, device, i, book.FileType, CutLength, OutDir, totalToDownload);
                }).Wait();
                
                sb.AppendLine($"下载完成 - {book.Title}");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    book.Done = true;
                    //Books.CollectionChanged
                });
           
                Log = sb.ToString();
                DownloadProgress = Convert.ToInt32(step * i+1);
                Thread.Sleep(1000);

            }

            sb.AppendLine("所有电子书下载完成");
            Log = sb.ToString();
        }

        private bool isInProgress;
        private bool isGetBooks;

        private KindleUrl kindleUrl;
        private DeviceResp.Device device;
        private int totalToDownload;
        private int startIndex = 0;
        private int batchSize = 100;

        private async void GetBookList()
        {
            if (isGetBooks)
                return;
            isGetBooks = true;

            if (string.IsNullOrWhiteSpace(Cookie) || string.IsNullOrWhiteSpace(CsrfToken))
            {
                MessageBox.Show("Cookie 和 CsrfToken 不能为空", "提示", MessageBoxButton.OK,MessageBoxImage.Warning);
                isGetBooks = false;
                return;
            }
            
            KindleConst.COOKIE = Cookie;
            kindleUrl = GetDomain(AmazonArea);
            var deviceResp = await HttpUtil.GetDevices(kindleUrl, CsrfToken);
            if (deviceResp == null)
            {
                MessageBox.Show("未能获取到 kindle 设备，请检查cookie", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                isGetBooks = false;
                return;
            }
            if (FileType == FileTypeEnum.PDOC)
            {
                batchSize = 18;
            }
            device = deviceResp.GetDevices.Devices[0];
            var resp = await HttpUtil.GetBookList(kindleUrl, batchSize, CsrfToken, startIndex,  FileType.ToString());
            if (resp == null)
            {
                isGetBooks = false;
                return;
            }
            totalToDownload = resp.OwnershipData.NumberOfItems;
            var items = resp.OwnershipData.Items;

            if (resp.OwnershipData.HasMoreItems)
            {
                while (true)
                {
                    startIndex += batchSize;
                    resp = await HttpUtil.GetBookList(kindleUrl, batchSize, CsrfToken, startIndex, FileType.ToString());
                    if (resp != null)
                    {
                        items.AddRange(resp.OwnershipData.Items);
                        if (!resp.OwnershipData.HasMoreItems)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Books.Clear();
            int index = 0;
            foreach (var item in items)
            {
                Books.Add(new BookItem() { Index = index, Author = item.Authors, Title = item.Title, Asin = item.Asin ,FileType = FileType.ToString() });
                index++;
            }
            isGetBooks = false;
        }

        private void initData()
        {
            _fileTypeEnum = FileTypeEnum.EBOK;
            _cutLength = 100;
            _outDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
        }

        private void SelectFileType(string obj)
        {
            if (isGetBooks)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(obj)) return;
            switch (obj)
            {
                case "EBOK":
                default:
                    FileType = FileTypeEnum.EBOK;
                    break;
                case "PDOC":
                    FileType = FileTypeEnum.PDOC;
                    break;
            }
        }

        private KindleUrl GetDomain(AmazonAreaEnum amazonArea)
        {
            switch(amazonArea)
            {
                case AmazonAreaEnum.CN:
                default:
                    return KindleUrl.CN;
                case AmazonAreaEnum.COM:
                    return KindleUrl.COM;
                case AmazonAreaEnum.JP:
                    return KindleUrl.JP;
            }
        }

        private FileTypeEnum _fileTypeEnum;
        public FileTypeEnum FileType { get { return _fileTypeEnum; } set { SetProperty(ref _fileTypeEnum, value); } }
        private string _cookies;
        public string Cookie { get { return _cookies; } set { SetProperty(ref _cookies, value); } }
        private string _csrfToken;
        public string CsrfToken { get { return _csrfToken; } set { SetProperty(ref _csrfToken, value); } }
        private int _cutLength;
        public int CutLength { get { return _cutLength; } set { SetProperty(ref _cutLength, value); } }
        private string _outDir;
        public string OutDir { get { return _outDir; } set { SetProperty( ref _outDir, value); } }
        private AmazonAreaEnum _amazonArea;
        public AmazonAreaEnum AmazonArea {  get { return _amazonArea; } set { SetProperty(ref _amazonArea, value); } }

        private int _downloadProgress;
        public int DownloadProgress { get { return _downloadProgress; } set { SetProperty(ref _downloadProgress, value); } }

        private string _log;
        public string Log { get { return _log; } set { SetProperty(ref _log, value); } }

        private ObservableCollection<BookItem> _books;
        public ObservableCollection<BookItem> Books
        {
            get { return _books; }
            set { SetProperty(ref _books, value); }
        }

        private void CreateBooks()
        {
            for (int i = 0; i < 100; i++)
            {
                Books.Add(new BookItem() { Index = i, Asin = "asid" + i, Author = "author" + i, Title = "book" + i, FileType= FileType.ToString() });
            }
            
        }

        public IRelayCommand<string> SelectFileTypeCommand { get; private set; }

        public IRelayCommand GetBookListCommand { get; private set; }

        public IRelayCommand DownBooksCommand { get; private set; }
    }
}
