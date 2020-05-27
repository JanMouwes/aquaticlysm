
namespace Resources
{
    public struct ResourceType
    {
        public string shortName;
        public string IconPath { get; set; }
        public string Description { get; set; }
        public ResourceType(string name, string iconPath = "", string description = "")
        {
            this.shortName = name;
            IconPath = iconPath;
            Description = description;
        }
    }
}
