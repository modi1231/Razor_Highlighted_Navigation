using System.Threading.Tasks;

namespace Razor_Highlighted_Navigation.Data
{
    public class DataAccess
    {
        /* Quick and dirty filling in of random types of modules, sections, content, etc.. Simulating what would be coming out of a DB.
        * The fun is the input.  If any of those variable ids match what is being added flip a flag so they are highlighted in the view of the HTML.
        * */
        public async Task<MyData> LoadAllNav(int M_ID, int S_ID, int C_ID)
        {
            MyData temp = new MyData();
            int contentID = 1;
            int sectionID = 1;
            int modID = 1;

            // connecting to a dB would replace all of this.  Keep in mind to check ids to flag as 'current'.
            await Task.Run(() =>
            {
            });
            Module tempMod = new Module();
            Section tempSec = new Section();
            Content tempCont = new Content();

            //a module object and fill in the data
            tempMod.ID = modID;
            tempMod.isCurrent = tempMod.ID == M_ID;//check that ID!
            modID += 1;//incremente to keep 'unique' ids

            //create sections 
            tempSec.ID = sectionID;
            tempSec.isCurrent = tempSec.ID == S_ID;//check that ID!
            sectionID += 1;//incremente to keep 'unique' ids

            //create content for the section.
            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;//check that ID!
            tempSec.content.Add(tempCont);//add content to the parent section.
            contentID += 1;//incremente to keep 'unique' ids


            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            tempMod.sections.Add(tempSec);

            tempSec = new Section();
            tempSec.ID = sectionID;
            tempSec.isCurrent = tempSec.ID == S_ID;
            sectionID += 1;

            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            tempMod.sections.Add(tempSec);


            temp.modules.Add(tempMod);
            //--------------------

            tempMod = new Module();
            tempMod.ID = modID;
            tempMod.isCurrent = tempMod.ID == M_ID;
            modID += 1;

            tempSec = new Section();
            tempSec.ID = sectionID;
            tempSec.isCurrent = tempSec.ID == S_ID;
            sectionID += 1;
 
            tempMod.sections.Add(tempSec);

            tempSec = new Section();
            tempSec.ID = sectionID;
            tempSec.isCurrent = tempSec.ID == S_ID;
            sectionID += 1;
            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            tempCont = new Content()
            {
                ID = contentID,
                Name = "Content"
            };
            tempCont.isCurrent = tempCont.ID == C_ID;
            contentID += 1;
            tempSec.content.Add(tempCont);

            contentID += 1;
            tempMod.sections.Add(tempSec);
            tempSec.isCurrent = tempSec.ID == S_ID;


            temp.modules.Add(tempMod);
 
            //================
            tempMod = new Module();
            tempMod.ID = modID;
            tempMod.isCurrent = tempMod.ID == M_ID;
            modID += 1;
            temp.modules.Add(tempMod);



            return temp;
        }
    }
}
