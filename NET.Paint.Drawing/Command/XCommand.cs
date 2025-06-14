using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.Drawing.Command
{
    public class XCommand
    {
        private XImage _image;
        public XCommand(XImage image) => _image = image;

        private RelayCommand? _addLayer;
        public ICommand AddLayer
        {
            get
            {
                if (_addLayer == null)
                    _addLayer = new RelayCommand(
                        () => _image.Layers.Add(new XLayer()),
                        () => true
                    );
                return _addLayer;
            }
        }

        private RelayCommand? _removeLayer;
        public ICommand RemoveLayer
        {
            get
            {
                if (_removeLayer == null)
                    _removeLayer = new RelayCommand(
                        () => _image.Layers.Remove(_image.Layers.Last()),
                        () => _image.Selected != null && _image.Layers.Count() > 1
                    );
                return _removeLayer;
            }
        }

        private RelayCommand? _undoLast;
        public ICommand UndoLast
        {
            get
            {
                if (_undoLast == null)
                    _undoLast = new RelayCommand(
                        () => {
                            _image.Undo.Push(_image.Layers.Last().Shapes.Last());
                            _image.Layers.Last().Shapes.Remove(_image.Layers.Last().Shapes.Last());
                        },
                        () => _image.Layers.Sum(x => x.Shapes.Count()) > 1
                    ); 
                return _undoLast;
            }
        }

        private RelayCommand? _redoLast;
        public ICommand RedoLast
        {
            get
            {
                if (_redoLast == null)
                    _redoLast = new RelayCommand(
                        () => _image.Layers.Last().Shapes.Add(_image.Undo.Pop()),
                        () => _image.Undo.History.Count() > 1
                    );
                return _redoLast;
            }
        }
    }
}
