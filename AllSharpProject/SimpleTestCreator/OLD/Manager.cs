using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace SimpleTestCreator.Common
{
    public static class Manager
    {
        private static Test test;
        private static string currentDirectory = @"C:\";

        public static Test Test
        {
            get { return Manager.test; }
        }

        public static void OpenTest()
        { 
        
        }

        public static void SaveTest()
        { 
        
        }

        public static void SaveTestAs()
        { 
        
        }

        public static void AddQuestion(QuestionBase q)
        { 
        
        }

        public static void RemoveQuestion(string Id)
        { 
        
        }

        public static void RemoveQuestion(int num)
        {

        }


        public static QuestionBase GetQuestion(int num)
        {
            return null;
        }

        public static QuestionBase GetQuestion(string id)
        {
            return null;
        }

        public static void ChangeQuestion(QuestionBase qNew)
        { 
        
        }
    }
}
