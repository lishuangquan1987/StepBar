using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_StepControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_StepControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_StepControl;assembly=WPF_StepControl"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class StepBar : ItemsControl
    {

        #region Progress

        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(StepBar), new PropertyMetadata(0));

        public Brush ActivateColor
        {
            get { return (Brush)GetValue(ActivateColorProperty); }
            set { SetValue(ActivateColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActivateColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActivateColorProperty =
            DependencyProperty.Register("ActivateColor", typeof(Brush), typeof(StepBar), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public Brush UnActivateColor
        {
            get { return (Brush)GetValue(UnActivateColorProperty); }
            set { SetValue(UnActivateColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnActivateColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnActivateColorProperty =
            DependencyProperty.Register("UnActivateColor", typeof(Brush), typeof(StepBar), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));


        /// <summary>
        /// 线的最小长度
        /// </summary>
        public double LineMinLength
        {
            get { return (double)GetValue(LineMinLengthProperty); }
            set { SetValue(LineMinLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineMinLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineMinLengthProperty =
            DependencyProperty.Register("LineMinLength", typeof(double), typeof(StepBar), new PropertyMetadata(50d));



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(StepBar), new PropertyMetadata(Orientation.Horizontal));



        #endregion


        #region Constructors
        static StepBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepBar), new FrameworkPropertyMetadata(typeof(StepBar)));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new StepBarItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            //设置Item的显示数字
            StepBarItem stepBarItem = element as StepBarItem;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(stepBarItem);
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(stepBarItem);
            stepBarItem.Number = Convert.ToString(++index);
            base.PrepareContainerForItemOverride(element, item);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            //ItemsControl数量变化时，重新设置各个Item的显示的数字
            for (int i = 0; i < this.Items.Count; i++)
            {
                StepBarItem stepBarItem = this.ItemContainerGenerator.ContainerFromIndex(i) as StepBarItem;
                if (stepBarItem != null)
                {
                    int temp = i;
                    stepBarItem.Number = Convert.ToString(++temp);
                }
            }
            //进度重新回到第一个
            //this.Progress = 0;
        }
        #endregion

    }
}
