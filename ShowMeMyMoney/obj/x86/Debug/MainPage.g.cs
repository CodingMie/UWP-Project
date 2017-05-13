﻿#pragma checksum "G:\作业\现操\期中\project\UWP-Project\ShowMeMyMoney\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F139036743667337BE5C8F54C23F8AB5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShowMeMyMoney
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_Border_Background(global::Windows.UI.Xaml.Controls.Border obj, global::Windows.UI.Xaml.Media.Brush value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), targetNullValue);
                }
                obj.Background = value;
            }
        };

        private class MainPage_obj16_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::ShowMeMyMoney.Model.categoryItem dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj16;
            private global::Windows.UI.Xaml.Controls.TextBlock obj17;
            private global::Windows.UI.Xaml.Controls.TextBlock obj18;

            public MainPage_obj16_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 16:
                        this.obj16 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.Border)target);
                        break;
                    case 17:
                        this.obj17 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 18:
                        this.obj18 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::ShowMeMyMoney.Model.categoryItem data = args.NewValue as global::ShowMeMyMoney.Model.categoryItem;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::ShowMeMyMoney.Model.categoryItem was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::ShowMeMyMoney.Model.categoryItem);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.Border)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::ShowMeMyMoney.Model.categoryItem) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // MainPage_obj16_Bindings

            public void SetDataRoot(global::ShowMeMyMoney.Model.categoryItem newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ShowMeMyMoney.Model.categoryItem obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_color(obj.color, phase);
                        this.Update_name(obj.name, phase);
                        this.Update_amount(obj.amount, phase);
                    }
                }
            }
            private void Update_color(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Border_Background(this.obj16.Target as global::Windows.UI.Xaml.Controls.Border, (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), obj), null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj17, obj, null);
                }
            }
            private void Update_amount(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj18, obj.ToString(), null);
                }
            }
        }

        private class MainPage_obj21_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::ShowMeMyMoney.Model.categoryItem dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj21;
            private global::Windows.UI.Xaml.Controls.TextBlock obj22;
            private global::Windows.UI.Xaml.Controls.TextBlock obj23;
            private global::Windows.UI.Xaml.Controls.TextBlock obj24;

            public MainPage_obj21_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 21:
                        this.obj21 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.Border)target);
                        break;
                    case 22:
                        this.obj22 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 23:
                        this.obj23 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 24:
                        this.obj24 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::ShowMeMyMoney.Model.categoryItem data = args.NewValue as global::ShowMeMyMoney.Model.categoryItem;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::ShowMeMyMoney.Model.categoryItem was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::ShowMeMyMoney.Model.categoryItem);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.Border)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::ShowMeMyMoney.Model.categoryItem) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // MainPage_obj21_Bindings

            public void SetDataRoot(global::ShowMeMyMoney.Model.categoryItem newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ShowMeMyMoney.Model.categoryItem obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_color(obj.color, phase);
                        this.Update_name(obj.name, phase);
                        this.Update_share(obj.share, phase);
                        this.Update_amount(obj.amount, phase);
                    }
                }
            }
            private void Update_color(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Border_Background(this.obj21.Target as global::Windows.UI.Xaml.Controls.Border, (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), obj), null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj22, obj, null);
                }
            }
            private void Update_share(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj23, obj.ToString(), null);
                }
            }
            private void Update_amount(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj24, obj.ToString(), null);
                }
            }
        }

        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::ShowMeMyMoney.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj12;
            private global::Windows.UI.Xaml.Controls.ListView obj13;
            private global::Windows.UI.Xaml.Controls.TextBlock obj28;

            private MainPage_obj1_BindingsTracking bindingsTracking;

            public MainPage_obj1_Bindings()
            {
                this.bindingsTracking = new MainPage_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 12:
                        this.obj12 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 13:
                        this.obj13 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 28:
                        this.obj28 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // MainPage_obj1_Bindings

            public void SetDataRoot(global::ShowMeMyMoney.MainPage newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ShowMeMyMoney.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_categoryViewModel(obj.categoryViewModel, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_totalExpense(obj.totalExpense, phase);
                    }
                }
            }
            private void Update_categoryViewModel(global::ShowMeMyMoney.ViewModel.categoryViewModel obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_categoryViewModel_AllExpenseCatagoryItems(obj.AllExpenseCatagoryItems, phase);
                        this.Update_categoryViewModel_AllIncomeCatagoryItems(obj.AllIncomeCatagoryItems, phase);
                    }
                }
            }
            private void Update_categoryViewModel_AllExpenseCatagoryItems(global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj12, obj, null);
                }
            }
            private void Update_categoryViewModel_AllIncomeCatagoryItems(global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj13, obj, null);
                }
            }
            private void Update_totalExpense(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj28, obj.ToString(), null);
                }
            }

            private class MainPage_obj1_BindingsTracking
            {
                global::System.WeakReference<MainPage_obj1_Bindings> WeakRefToBindingObj; 

                public MainPage_obj1_BindingsTracking(MainPage_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<MainPage_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                }

                public void PropertyChanged_categoryViewModel_AllExpenseCatagoryItems(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged_categoryViewModel_AllExpenseCatagoryItems(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem>;
                    }
                }
                public void PropertyChanged_categoryViewModel_AllIncomeCatagoryItems(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged_categoryViewModel_AllIncomeCatagoryItems(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::ShowMeMyMoney.Model.categoryItem>;
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.myTitle = (global::Windows.UI.Xaml.Style)(target);
                }
                break;
            case 3:
                {
                    this.AddNewCategoryDialog = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    #line 181 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.AddNewCategoryDialog).PrimaryButtonClick += this.AddNewCategoryDialog_PrimaryButtonClick;
                    #line 182 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.AddNewCategoryDialog).SecondaryButtonClick += this.AddNewCategoryDialog_SecondaryButtonClick;
                    #line default
                }
                break;
            case 4:
                {
                    this.categoryName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.shareSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 191 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)this.shareSlider).ValueChanged += this.shareSlider_ValueChanged;
                    #line default
                }
                break;
            case 6:
                {
                    this.ColorPallete_Combo = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 194 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ColorPallete_Combo).SelectionChanged += this.ColorPalleteCombo_SelectionChanged;
                    #line default
                }
                break;
            case 7:
                {
                    this.categoryColor = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8:
                {
                    this.expenseButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 185 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.expenseButton).Checked += this.out_Checked;
                    #line default
                }
                break;
            case 9:
                {
                    this.incomeButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 186 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.incomeButton).Checked += this.in_Checked;
                    #line default
                }
                break;
            case 10:
                {
                    this.shareBar = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 11:
                {
                    this.表头 = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 12:
                {
                    this.expenseList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 104 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.expenseList).ItemClick += this.ShowCategory_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.incomeList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 142 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.incomeList).ItemClick += this.ShowCategory_Click;
                    #line default
                }
                break;
            case 14:
                {
                    this.AddNewCategoryButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 165 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.AddNewCategoryButton).Click += this.AddNewCategoryButton_Click;
                    #line default
                }
                break;
            case 15:
                {
                    this.AddNewAccountButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 170 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.AddNewAccountButton).Click += this.AddNewAccountButton_Click;
                    #line default
                }
                break;
            case 19:
                {
                    this.TotalIncomeItem = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 20:
                {
                    this.TotalIncomeAmount = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 25:
                {
                    this.PocketMoneyItem = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 26:
                {
                    this.PocketMoneyAmount = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 27:
                {
                    this.TotalExpenseItem = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 28:
                {
                    this.TotalExpenseAmount = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 29:
                {
                    this.TotalBudgetItem = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 30:
                {
                    this.TotalBudgetProportion = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.Border element16 = (global::Windows.UI.Xaml.Controls.Border)target;
                    MainPage_obj16_Bindings bindings = new MainPage_obj16_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::ShowMeMyMoney.Model.categoryItem) element16.DataContext);
                    element16.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element16, bindings);
                }
                break;
            case 21:
                {
                    global::Windows.UI.Xaml.Controls.Border element21 = (global::Windows.UI.Xaml.Controls.Border)target;
                    MainPage_obj21_Bindings bindings = new MainPage_obj21_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::ShowMeMyMoney.Model.categoryItem) element21.DataContext);
                    element21.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element21, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

