
namespace Resources
{
    public struct ResourceType
    {
        public string shortName;
        public string IconPath { get; set; }
        
        public ResourceType(string name, string iconPath = "")
        {
            this.shortName = name;
            IconPath = iconPath;
        }
    }
}
