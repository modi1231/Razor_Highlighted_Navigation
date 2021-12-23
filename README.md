Razor_Highlighted_Navigation
Tutorial illustrating highlightable bread crumbs for three tier document index

https://www.dreamincode.net/forums/topic/417443-razor-pages-core-21-breadcrumb-navigation/



=================
dreamincode.net tutorial backup ahead of decommissioning


 Posted 30 September 2019 - 01:35 PM 
 
 Occasionally one may find themselves needing to make a clickable index for navigation, document access, etc.  This could be a fairly labor intensive to highlight chapters, sections, and modules or make a coherent breadcrumb so the user knows where they are in the work.

A little bit of planning, and Razor witchcraft, and this problem can be laid out bare and ready for modification.

[img]https://i.imgur.com/ri5sSJX.jpg[/img]

[u]Software:[/u]
-- Visual Studios 2019

[u]Concepts:[/u]
-- C#
-- Razor Pages

[u]Github link:[/u] https://github.com/modi1231/Razor_Highlighted_Navigation

The perfunctory work is an empty ASP.NET Core project.  I fixed up the startup to include MVC and static pages, added the folders for pages, shared, and data, and added the usual cruft that goes with it.

This is all contained in the 'index' page.


I started with the data model.  It was recommended by a friend to look at something that would be a few layers deep.  Say a list of modules, and each module could have a list of sections.  Each section could have a list of contents.

Each object would have to have a unique ID (assuming this being pulled from a database), a collection of the objects it would contain, and a flag to indicate if a user selected it or not.


[code]
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
[/code]


There's room for addition and improvement, but at the core of this exercise - to highlight specific sections, and parents, when clicked - this boils down the topic well enough.


I shifted to thinking about how to get the user information of what was clicked.  I would need to track the module id of what was clicked, the section and module ids if a section was clicked, or the content, section, and module ids if a content was clicked.

I tweaked the 'on get' to look for those three variables.  On the plus side a fresh view of the page will fill with zeros and none of the 'ids' should be zero.  Happy coincidences are happy.

[code]        
public MyData foo { get; set; }

public async Task OnGetAsync(int M_ID, int S_ID, int C_ID)[/code]

Loading the data is a little more difficult to see as I am kludging in temporary data.  I want to load the data, but if the ID of any of those chunks being loaded matches what clicked then flag it as 'selected'.

[code]        public MyData foo { get; set; }

        public async Task OnGetAsync(int M_ID, int S_ID, int C_ID)
        {          
            DataAccess da = new DataAccess();

            foo = await da.LoadAllNav(M_ID, S_ID, C_ID);

        }
[/code]


The 'data access' is pretty boring.  The only important part is to check for matching IDs and make 'current' or active.  

[code]
    public class DataAccess
    {
        public async Task<MyData> LoadAllNav(int M_ID, int S_ID, int C_ID)
        {
[/code]


The view part is pretty interesting.  It uses nested for loops to go through the onion layers of data, setting up clickable links, filling route ids to hand back to the 'on get', and conditionally tweaking the class to jack with the display if the content/section/module, section/module, or just module were clicked.

Page handler says where to go when clicked.
Route provides variable names to fill when there
The module's ID provides the data to shuffle in on that route.

The class is conditional to be the be what was known above, or empty.

[code]<form method="post">

    @for (int i = 0; i < Model.foo.modules.Count; i++)
    {
        <a asp-page-handler="index"
           asp-route-M_ID="@Model.foo.modules[i].ID"
           class="@(Model.foo.modules[i].isCurrent == true ? "Active" : "")">
            Module: @Model.foo.modules[i].ID
        </a><br />
[/code]

The section expands on the module but adds a new route id for the section id.  This means if a section is clicked then the parent is also shown to be highlighted too!  You could totally omit the 'module' id section in there to not have that happen.

[code]    @for (int i = 0; i < Model.foo.modules.Count; i++)
    {
        <a asp-page-handler="index" 
           asp-route-M_ID="@Model.foo.modules[i].ID"
           class="@(Model.foo.modules[i].isCurrent == true ? "Active" : "")">
            Module: @Model.foo.modules[i].ID
        </a><br />

        for (int s = 0; s < Model.foo.modules[i].sections.Count; s++)
        {
            <a asp-page-handler="index"
               asp-route-M_ID="@Model.foo.modules[i].ID"
               asp-route-S_ID="@Model.foo.modules[i].sections[s].ID"
               class="@(Model.foo.modules[i].sections[s].isCurrent == true ? "Active" : "")">
                ... Section: @Model.foo.modules[i].sections[s].ID
            </a><br />
[/code]

Lastly the content expands to now have a route variable for it's id.

[code]    @for (int i = 0; i < Model.foo.modules.Count; i++)
    {
        <a asp-page-handler="index" 
           asp-route-M_ID="@Model.foo.modules[i].ID"
           class="@(Model.foo.modules[i].isCurrent == true ? "Active" : "")">
            Module: @Model.foo.modules[i].ID
        </a><br />

        for (int s = 0; s < Model.foo.modules[i].sections.Count; s++)
        {
            <a asp-page-handler="index"
               asp-route-M_ID="@Model.foo.modules[i].ID"
               asp-route-S_ID="@Model.foo.modules[i].sections[s].ID"
               class="@(Model.foo.modules[i].sections[s].isCurrent == true ? "Active" : "")">
                ... Section: @Model.foo.modules[i].sections[s].ID
            </a><br />

            for (int c = 0; c < Model.foo.modules[i].sections[s].content.Count; c++)
            {
                <a asp-page-handler="index"
                   asp-route-M_ID="@Model.foo.modules[i].ID"
                   asp-route-S_ID="@Model.foo.modules[i].sections[s].ID"
                   asp-route-C_ID="@Model.foo.modules[i].sections[s].content[c].ID"
                   class="@(Model.foo.modules[i].sections[s].content[c].isCurrent == true ? "Active" : "")">
                    ...... @Model.foo.modules[i].sections[s].content[c].Name @Model.foo.modules[i].sections[s].content[c].ID
[/code]

After the the for loops cycle and should know what was clicked and what to highlight.

This sort of self contained logic could be plopped, gracefully, in most projects that need this sort of navigation and bread crumbs.  It also could be expanded to be in drop down JS UI and so forth.  The world's your oyster - go shuck it!
