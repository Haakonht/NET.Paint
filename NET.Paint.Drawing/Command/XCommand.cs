using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using NET.Paint.Drawing.Service;
using System.Windows.Input;

namespace NET.Paint.Drawing.Command
{
    public class XCommand
    {
        public XOperations Operations { get; set; }
        public XCommand(XService service) => Operations = new XOperations(service);

        #region Edit Commands

        private ICommand _copy;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                    _copy = new RelayCommand(ExecuteCopy, CanExecuteCopy);
                return _copy;
            }
        }
        private void ExecuteCopy(object parameter) => Operations.Copy(parameter);
        private bool CanExecuteCopy(object parameter) => parameter is XRenderable || parameter is XLayer;

        private ICommand _cut;
        public ICommand Cut
        {
            get
            {
                if (_cut == null)
                    _cut = new RelayCommand(ExecuteCut, CanExecuteCut);
                return _cut;
            }
        }
        private void ExecuteCut(object parameter) => Operations.Cut(parameter);
        private bool CanExecuteCut(object parameter) => parameter is XRenderable || parameter is XLayer;

        private ICommand _paste;
        public ICommand Paste
        {
            get
            {
                if (_paste == null)
                    _paste = new RelayCommand(ExecutePaste, CanExecutePaste);
                return _paste;
            }
        }
        private void ExecutePaste(object parameter) => Operations.Paste(parameter);
        private bool CanExecutePaste(object parameter) => parameter is XVectorLayer || parameter is XRenderable;

        #endregion

        #region Tree Commands

        private ICommand _removeItem;
        public ICommand RemoveItem 
        { 
            get
            {
                if (_removeItem == null)
                    _removeItem = new RelayCommand(ExecuteRemoveItem, CanExecuteRemoveItem);
                return _removeItem;
            } 
        }

        private void ExecuteRemoveItem(object parameter)
        {
            if (parameter is XImage image)
                Operations.RemoveImage(image);

            if (parameter is XVectorLayer layer)
                Operations.RemoveLayer(layer);

            if (parameter is XRenderable renderable)
                Operations.RemoveRenderable(renderable);          
        }

        private bool CanExecuteRemoveItem(object parameter) => parameter is XImage || parameter is XLayer || parameter is XRenderable;

        private ICommand _addItem;
        public ICommand AddItem
        {
            get
            {
                if (_addItem == null)
                    _addItem = new RelayCommand(ExecuteAddItem, CanExecuteAddItem);
                return _addItem;
            }
        }

        private void ExecuteAddItem(object parameter)
        {
            if (parameter is XImage image)
                Operations.CreateImage(image);

            if (parameter is XLayer layer)
                Operations.CreateLayer();
        }

        private bool CanExecuteAddItem(object parameter) => parameter is XImage || parameter is XLayer;

        #endregion
    }
}
