using System.Collections.Generic;

namespace Razor_Highlighted_Navigation.Data
{
    /* Data looks as follows.
     * Main root of 'my data'.
     * In there is a list of modules.
     * Each module contains a unique id, zero to many list of section objects, and 'is current' flag
     * Each section contains a unique id, zero to many list of content objects, and 'is current' flag
     * Each content contains a unique id, some text, and 'is current' flag
     * */

    public class MyData
    {
        public List<Module> modules { get; set; }

        public MyData()
        {
            modules = new List<Module>();
        }
    }
    public class Module
    {
        public int ID { get; set; }
        public List<Section> sections { get; set; }
        public bool isCurrent { get; set; }
        public Module()
        {
            sections = new List<Section>();
        }

    }
    public class Section
    {
        public int ID { get; set; }
        public List<Content> content { get; set; }
        public bool isCurrent { get; set; }

        public Section()
        {
            content = new List<Content>();
        }

    }
    public class Content
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isCurrent { get; set; }
    }
}
