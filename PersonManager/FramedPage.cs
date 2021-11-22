using System.Windows.Controls;
using Zadatak.ViewModels;

namespace Zadatak
{

    public class FramedPage<T> : Page
    {
        public FramedPage(T viewModel)
        {
            ViewModel = viewModel;
        }
        public T ViewModel { get; }
        public Frame Frame { get; set; }
    }
}
