using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using NET.Paint.Drawing.Service;
using System.Windows.Input;

namespace NET.Paint.Drawing.Command
{
    public class XCommand
    {
        public XOperations Operations { get; set; }
        public XCommand(XService service) => Operations = new XOperations(service);

        #region History Commands

        private ICommand _undo;
        public ICommand Undo
        {
            get
            {
                if (_undo == null)
                    _undo = new RelayCommand(ExecuteUndo, CanExecuteUndo);
                return _undo;
            }
        }
        private void ExecuteUndo(object parameter) => Operations.Undo();
        private bool CanExecuteUndo(object parameter) => 
            Operations._service.ActiveImage?.ActiveLayer?.CanUndo ?? false;

        private ICommand _redo;
        public ICommand Redo
        {
            get
            {
                if (_redo == null)
                    _redo = new RelayCommand(ExecuteRedo, CanExecuteRedo);
                return _redo;
            }
        }
        private void ExecuteRedo(object parameter) => Operations.Redo();
        private bool CanExecuteRedo(object parameter) => 
            Operations._service.ActiveImage?.Undo.CanRedo ?? false;

        #endregion

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
        private void ExecuteCopy(object parameter) => 
            Operations.Copy(Operations._service.ActiveImage?.Selected);
        private bool CanExecuteCopy(object parameter) => 
            Operations._service.ActiveImage?.CanCopy ?? false;

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
        private void ExecuteCut(object parameter) => 
            Operations.Cut(Operations._service.ActiveImage?.Selected);
        private bool CanExecuteCut(object parameter) => 
            Operations._service.ActiveImage?.CanCut ?? false;

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
        private bool CanExecutePaste(object parameter) => 
            XClipboard.Instance.CanPaste && Operations._service.ActiveImage?.ActiveLayer != null;

        private ICommand _clearClipboard;
        public ICommand ClearClipboard
        {
            get
            {
                if (_clearClipboard == null)
                    _clearClipboard = new RelayCommand(ExecuteClearClipboard, CanExecuteClearClipboard);
                return _clearClipboard;
            }
        }
        private void ExecuteClearClipboard(object parameter) => Operations.ClearClipboard();
        private bool CanExecuteClearClipboard(object parameter) => XClipboard.Instance.Data.Count > 0;

        #endregion

        #region Project Commands

        private ICommand _createProject;
        public ICommand CreateProject
        {
            get
            {
                if (_createProject == null)
                    _createProject = new RelayCommand(ExecuteCreateProject, CanExecuteCreateProject);
                return _createProject;
            }
        }
        private void ExecuteCreateProject(object parameter) => Operations.CreateProject();
        private bool CanExecuteCreateProject(object parameter) => true;

        private ICommand _openProject;
        public ICommand OpenProject
        {
            get
            {
                if (_openProject == null)
                    _openProject = new RelayCommand(ExecuteOpenProject, CanExecuteOpenProject);
                return _openProject;
            }
        }
        private void ExecuteOpenProject(object parameter) => Operations.OpenProject();
        private bool CanExecuteOpenProject(object parameter) => true;

        private ICommand _saveProject;
        public ICommand SaveProject
        {
            get
            {
                if (_saveProject == null)
                    _saveProject = new RelayCommand(ExecuteSaveProject, CanExecuteSaveProject);
                return _saveProject;
            }
        }
        private void ExecuteSaveProject(object parameter) => Operations.SaveProject();
        private bool CanExecuteSaveProject(object parameter) => Operations._service.Project != null;

        #endregion

        #region Image Commands

        private ICommand _exportImage;
        public ICommand ExportImage
        {
            get
            {
                if (_exportImage == null)
                    _exportImage = new RelayCommand(ExecuteExportImage, CanExecuteExportImage);
                return _exportImage;
            }
        }
        private void ExecuteExportImage(object parameter)
        {
            if (parameter is XImage image)
                Operations.ExportImage(image);
            else if (Operations._service.ActiveImage != null)
                Operations.ExportImage(Operations._service.ActiveImage);
        }
        private bool CanExecuteExportImage(object parameter) => 
            parameter is XImage || Operations._service.ActiveImage != null;

        private ICommand _moveImage;
        public ICommand MoveImage
        {
            get
            {
                if (_moveImage == null)
                    _moveImage = new RelayCommand(ExecuteMoveImage, CanExecuteMoveImage);
                return _moveImage;
            }
        }
        private void ExecuteMoveImage(object parameter)
        {
            if (parameter is (XImage imageToMove, XImage targetImage))
                Operations.MoveImage(Operations._service.Project, imageToMove, targetImage);
        }
        private bool CanExecuteMoveImage(object parameter) => parameter is (XImage, XImage);

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
            else if (parameter is XLayer layer)
                Operations.RemoveLayer(layer);
            else if (parameter is XRenderable renderable)
                Operations.RemoveRenderable(renderable);          
        }

        private bool CanExecuteRemoveItem(object parameter) => 
            parameter is XImage || parameter is XLayer || parameter is XRenderable;

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
            else if (parameter is XLayer layer)
                Operations.CreateLayer(layer);
        }

        private bool CanExecuteAddItem(object parameter) => parameter is XImage || parameter is XLayer;

        #endregion

        #region Layer Commands

        private ICommand _flattenLayer;
        public ICommand FlattenLayer
        {
            get
            {
                if (_flattenLayer == null)
                    _flattenLayer = new RelayCommand(ExecuteFlattenLayer, CanExecuteFlattenLayer);
                return _flattenLayer;
            }
        }
        private void ExecuteFlattenLayer(object parameter)
        {
            if (parameter is XLayer layer)
                Operations.FlattenLayer(Operations._service.ActiveImage, layer);
        }
        private bool CanExecuteFlattenLayer(object parameter) => 
            parameter is XVectorLayer && Operations._service.ActiveImage != null;

        private ICommand _moveLayer;
        public ICommand MoveLayer
        {
            get
            {
                if (_moveLayer == null)
                    _moveLayer = new RelayCommand(ExecuteMoveLayer, CanExecuteMoveLayer);
                return _moveLayer;
            }
        }
        private void ExecuteMoveLayer(object parameter)
        {
            if (parameter is (XLayer layerToMove, XLayer targetLayer))
                Operations.MoveLayer(Operations._service.ActiveImage, layerToMove, targetLayer);
        }
        private bool CanExecuteMoveLayer(object parameter) => 
            parameter is (XLayer, XLayer) && Operations._service.ActiveImage != null;

        private ICommand _moveLayerToImage;
        public ICommand MoveLayerToImage
        {
            get
            {
                if (_moveLayerToImage == null)
                    _moveLayerToImage = new RelayCommand(ExecuteMoveLayerToImage, CanExecuteMoveLayerToImage);
                return _moveLayerToImage;
            }
        }
        private void ExecuteMoveLayerToImage(object parameter)
        {
            if (parameter is (XLayer layer, XImage targetImage))
                Operations.MoveLayerToImage(Operations._service.Project, layer, targetImage);
        }
        private bool CanExecuteMoveLayerToImage(object parameter) => parameter is (XLayer, XImage);

        #endregion

        #region Shape Commands

        private ICommand _moveShapeToLayer;
        public ICommand MoveShapeToLayer
        {
            get
            {
                if (_moveShapeToLayer == null)
                    _moveShapeToLayer = new RelayCommand(ExecuteMoveShapeToLayer, CanExecuteMoveShapeToLayer);
                return _moveShapeToLayer;
            }
        }
        private void ExecuteMoveShapeToLayer(object parameter)
        {
            if (parameter is (XRenderable shape, XLayer targetLayer))
                Operations.MoveShapeToLayer(Operations._service.ActiveImage, shape, targetLayer);
        }
        private bool CanExecuteMoveShapeToLayer(object parameter) => 
            parameter is (XRenderable, XLayer) && Operations._service.ActiveImage != null;

        private ICommand _moveShapeInFrontOfShape;
        public ICommand MoveShapeInFrontOfShape
        {
            get
            {
                if (_moveShapeInFrontOfShape == null)
                    _moveShapeInFrontOfShape = new RelayCommand(ExecuteMoveShapeInFrontOfShape, CanExecuteMoveShapeInFrontOfShape);
                return _moveShapeInFrontOfShape;
            }
        }
        private void ExecuteMoveShapeInFrontOfShape(object parameter)
        {
            if (parameter is (XRenderable shapeToMove, XRenderable targetShape))
                Operations.MoveShapeInFrontOfShape(Operations._service.ActiveImage, shapeToMove, targetShape);
        }
        private bool CanExecuteMoveShapeInFrontOfShape(object parameter) => 
            parameter is (XRenderable, XRenderable) && Operations._service.ActiveImage != null;

        #endregion
    }
}
