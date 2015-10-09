using System.Collections.Generic;

namespace Assets.Scripts {
    public class Test {
        public static void Main(string[] args) {
            var test = new MyTest();
            var list = test.List;
            list[0] = 100;
            test.Print();
        } 
    }

    public class MyTest {
        private List<int> list = new List<int>() { 1, 2, 3, 4 };

        public List<int> List {
            get { return list; }
        }

        public void Print() {
            System.Console.WriteLine(list);
        }
    }
}