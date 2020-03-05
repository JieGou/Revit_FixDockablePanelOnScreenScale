namespace Revit_FixDockablePanelOnScreenScale
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ScreenScaleDecorator : Decorator
    {
        public ScreenScaleDecorator()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource != null)
            {
                if (presentationSource.CompositionTarget != null)
                {
                    Matrix m = presentationSource.CompositionTarget.TransformFromDevice;
                    ScaleTransform dpiTransform = new ScaleTransform(m.M11, m.M22);
                    if (dpiTransform.CanFreeze)
                        dpiTransform.Freeze();
                    RenderTransform = dpiTransform;

                    // 缩小文字
                    if (Parent is Page page)
                        page.FontSize *= 1 / m.M11;
                }
                //请务必取消订阅，否则装饰器将反复触发，
                //关闭所有文档并打开一个新文档时。面板内容
                //将按指数比例缩放
                Loaded -= OnLoaded;
            }

        }
    }
}
