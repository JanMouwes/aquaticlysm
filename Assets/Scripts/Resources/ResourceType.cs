
namespace Resources
{
    public struct ResourceType
    {
        public string ShortName { get; set; }
        public string IconPath { get; set; }
        public string Description { get; set; }
        public ResourceType(string name, string iconPath = "", string description = "")
        {
            this.ShortName = name;
            IconPath = iconPath;
            Description = description;
        }
    }
}
