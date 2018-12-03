using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLinq
{
    class Students
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Course { get; set; }
        public int Group { get; set; }
        public string Post { get; set; }

        public Students(string FirstName, string LastName, short Course, int Group, string Post)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Course = Course;
            this.Group = Group;
            this.Post = Post;
        }
        public string Show()
        {
            return String.Format("{0} {1}: {2}, курс {3} ({4})",
                        this.FirstName,
                        this.LastName,
                        this.Post,
                        this.Course,
                        this.Group
                    );
        }
    }
}
