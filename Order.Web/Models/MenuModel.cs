using System.Collections.Generic;
using System.Linq;

namespace WasherWeb.Areas.Owners.Models
{
    public static class MenuModelExtensions
    {
        public static bool HaveInMenu(this CommonMenuModel menuModel, string controller, out int menuOrder, out string menuName)
        {
            menuOrder = -1;
            menuName = string.Empty;

            string lowerController = controller.ToLower();

            if(menuModel is ParentMenuModel parentMenu)
            {
                var childModel = parentMenu.ChildModels.FirstOrDefault(x => x.Controller.ToLower() == lowerController);

                if (childModel == null)
                    return false;

                menuName = childModel.Name;
                menuOrder = childModel.Order;
            }
            else if(menuModel is MenuModel mainMenuModel)
            {
                if(mainMenuModel.Controller.ToLower() == lowerController)
                {
                    menuOrder = mainMenuModel.Order;
                    menuName = mainMenuModel.Name;
                }
            }

            return menuOrder != -1;
        }
    }

    public class CommonMenuModel
    {
        public CommonMenuModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class ParentMenuModel : CommonMenuModel
    {
        public ParentMenuModel(string name, List<MenuModel> childModels) : base(name)
        {
            ChildModels = childModels;
        }

        public List<MenuModel> ChildModels { get; set; } = new List<MenuModel>();
    }

    public class MenuModel : CommonMenuModel
    {
        public MenuModel(int order, string name, string action, string controller) : base(name)
        {
            Order = order;
            Action = action;
            Controller = controller;
        }


        public int Order { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
