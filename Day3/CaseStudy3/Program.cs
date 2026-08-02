using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CourseCategory starter = new CourseCategory("Starter");
            CourseCategory mainCourse = new CourseCategory("Main Course");
            CourseCategory dessert = new CourseCategory("Dessert");

            Menu regularMenu = new Menu("Family Menu", false, 0, new List<MenuItem>
            {
                new MenuItem("Tomato Soup", starter, 120m),
                new MenuItem("Paneer Tikka", starter, 180m),
                new MenuItem("Veg Biryani", mainCourse, 240m),
                new MenuItem("Butter Naan", mainCourse, 50m),
                new MenuItem("Gulab Jamun", dessert, 90m)
            });

            Menu specialMenu = new Menu("Weekend Special", true, 30, new List<MenuItem>
            {
                new MenuItem("Sweet Corn Soup", starter, 130m),
                new MenuItem("Schezwan Noodles", mainCourse, 220m),
                new MenuItem("Brownie", dessert, 110m)
            });

            RestaurantBranch branch = new RestaurantBranch("Central Branch", "Bangalore", new List<Menu> { regularMenu, specialMenu });
            Restaurant restaurant = new Restaurant("Spice Garden", new List<RestaurantBranch> { branch });

            Console.WriteLine("Case Study 3");
            Console.WriteLine("Total number of menu items: " + restaurant.GetTotalMenuItems());
            PrintItems("Items in Main Course", restaurant.GetMenuItemsByCourseCategory("Main Course"));
            PrintMenus("Special discount menus", restaurant.GetSpecialDiscountMenus());
        }

        private static void PrintItems(string title, IEnumerable<MenuItem> items)
        {
            Console.WriteLine(title);
            foreach (MenuItem item in items)
            {
                Console.WriteLine("- " + item.Name + " (" + item.Price.ToString("C") + ")");
            }
        }

        private static void PrintMenus(string title, IEnumerable<Menu> menus)
        {
            Console.WriteLine(title);
            foreach (Menu menu in menus)
            {
                Console.WriteLine("- " + menu.Name + " with " + menu.DiscountPercentage + "% discount");
            }
        }
    }

    public class CourseCategory
    {
        public CourseCategory(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class MenuItem
    {
        public MenuItem(string name, CourseCategory category, decimal price)
        {
            Name = name;
            Category = category;
            Price = price;
        }

        public string Name { get; private set; }
        public CourseCategory Category { get; private set; }
        public decimal Price { get; private set; }
    }

    public class Menu
    {
        public Menu(string name, bool isSpecial, decimal discountPercentage, List<MenuItem> items)
        {
            Name = name;
            IsSpecial = isSpecial;
            DiscountPercentage = discountPercentage;
            Items = items;
        }

        public string Name { get; private set; }
        public bool IsSpecial { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public List<MenuItem> Items { get; private set; }
    }

    public class RestaurantBranch
    {
        public RestaurantBranch(string name, string location, List<Menu> menus)
        {
            Name = name;
            Location = location;
            Menus = menus;
        }

        public string Name { get; private set; }
        public string Location { get; private set; }
        public List<Menu> Menus { get; private set; }
    }

    public class Restaurant
    {
        private readonly List<RestaurantBranch> _branches;

        public Restaurant(string name, List<RestaurantBranch> branches)
        {
            Name = name;
            _branches = branches;
        }

        public string Name { get; private set; }

        public int GetTotalMenuItems()
        {
            return _branches.SelectMany(delegate(RestaurantBranch branch) { return branch.Menus; }).SelectMany(delegate(Menu menu) { return menu.Items; }).Count();
        }

        public List<MenuItem> GetMenuItemsByCourseCategory(string categoryName)
        {
            return _branches.SelectMany(delegate(RestaurantBranch branch) { return branch.Menus; })
                .SelectMany(delegate(Menu menu) { return menu.Items; })
                .Where(delegate(MenuItem item) { return item.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase); })
                .ToList();
        }

        public List<Menu> GetSpecialDiscountMenus()
        {
            return _branches.SelectMany(delegate(RestaurantBranch branch) { return branch.Menus; })
                .Where(delegate(Menu menu) { return menu.IsSpecial; })
                .ToList();
        }
    }
}