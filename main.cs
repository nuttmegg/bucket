using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bucket {
    public class Bucket {
        public List<object> contents;

        public Bucket(params object[] inputs) {
            contents = new List<object>(255);

            foreach ( object thing in inputs ) contents.Add(thing);
        }


        // length
        public int length { get { return contents.Count; } }
        public int size { get { return contents.Count; } }


        // get
        public object get(int index) { return contents[index]; }
        public object fetch(int index) { return contents[index]; }
        public object at(int index) { return contents[index]; }


        // delete
        public object delete(object entry) {
            if (entry is int) {
                int index = Convert.ToInt32(entry);
                object thing = contents[index];
                contents.RemoveAt(index);
                return thing;
            }
            else if (entry is string) {
                int index = contents.IndexOf(entry);
                object thing = contents[index];
                contents.RemoveAt(index);
                return thing;
            }

            else return null;
        }
        public object remove(object entry) {
            if (entry is int) {
                int index = Convert.ToInt32(entry);
                object thing = contents[index];
                contents.RemoveAt(index);
                return thing;
            }
            else if (entry is string) {
                int index = contents.IndexOf(entry);
                object thing = contents[index];
                contents.RemoveAt(index);
                return thing;
            }

            else return null;
        }


        // push
        public void push(params object[] stuff ) { foreach ( object thing in stuff ) contents.Add(thing); }
        public void add(params object[] stuff ) { foreach ( object thing in stuff ) contents.Add(thing); }
        public void push_back(params object[] stuff ) { foreach ( object thing in stuff ) contents.Add(thing); }


        // pull
        public void pull(params object[] stuff ) { foreach ( object thing in stuff) contents.Insert(0, thing); }
        public void unshift(params object[] stuff ) { foreach ( object thing in stuff) contents.Insert(0, thing); }
        public void push_front(params object[] stuff ) { foreach ( object thing in stuff) contents.Insert(0, thing); }


        // set
        public void set(object entry, object value) { 
            if (entry is int) {
                int index = Convert.ToInt32(entry);
                contents[index] = value;
            }
            else if (entry is string) {
                int index = contents.IndexOf(entry);
                contents[index] = value;
            }
        }


        // pop
        public object pop(int offset = 0) { object thing = contents[contents.Count-offset-1]; contents.RemoveAt( contents.Count-offset-1 ); return thing; }
        public object unpush(int offset = 0) { object thing = contents[contents.Count-offset-1]; contents.RemoveAt( contents.Count-offset-1 ); return thing; }


        // shift
        public object shift(int offset = 0) { object thing = contents[0+offset]; contents.RemoveAt( 0+offset ); return thing; }
        public object unpull(int offset = 0) { object thing = contents[0+offset]; contents.RemoveAt( 0+offset ); return thing; }


        // forEach
        public void forEach( Action<object, int> func ) {
            for (int i = 0; i < length; i++) {
                func( get(i), i );
            }
        }


        // indexOf
        public int indexOf(object thing) { return contents.IndexOf(thing); }


        // lastIndexOf
        public int lastIndexOf(object thing) { return contents.LastIndexOf(thing); }


        // join
        public string join(string joiner=",") { return string.Join(joiner, contents.ToArray()); }


        // toString
        public string toString() { return string.Join(",", contents.ToArray()); }


        // includes
        public bool includes(params object[] stuff) {
            foreach ( object inc in stuff ) {
                for (int i = 0; i < length; i++) {
                    if (get(i) is string && get(i).ToString() == inc.ToString()) {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool contains(params object[] stuff) {
            foreach ( object inc in stuff ) {
                for (int i = 0; i < length; i++) {
                    if (get(i) is string && get(i).ToString() == inc.ToString()) {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool has(params object[] stuff) {
            foreach ( object inc in stuff ) {
                for (int i = 0; i < length; i++) {
                    if (get(i) is string && get(i).ToString() == inc.ToString()) {
                        return true;
                    }
                }
            }
            return false;
        }


        // clear
        public void clear() { contents.Clear(); }



        // filter
        public Bucket filter( Func<object, int, bool> func ) {
            List<object> newContents = new List<object>(255);

            for (int i = 0; i < length; i++ ) {
                if ( func( get(i), i ) ) {
                    newContents.Add(get(i));
                }
            }

            return new Bucket(newContents.ToArray());
        }


        // map
        public Bucket map( Func<object, int, object> func ) {
            Bucket newContents = new Bucket(contents.ToArray());

            for (int i = 0; i < newContents.length; i++) {
                newContents.set(i, func( get(i), i ));
            }

            return newContents;
        }


        // some
        public bool some(Func<object, int, bool> func) {
            for (int i = 0; i < length; i++) {
                if (func( get(i), i )) {
                    return true;
                }
            }
            return false;
        }


        // every
        public bool every(Func<object, int, bool> func) {
            for (int i = 0; i < length; i++) {
                if (!func( get(i), i )) {
                    return false;
                }
            }
            return true;
        }


        // reverse
        public void reverse() {
            contents.Reverse();
        }


        // read
        public string read() {
            Bucket newContents = new Bucket(contents.ToArray());

            char q = '"';

            newContents = newContents.map( (v, i) => {
                return ( v is string || v is char ) ? String.Format("{0}{1}{0}", q, v) : v;
            });

            return "{"+newContents.join(", ")+"}";
        }


        // front
        public List<object> front(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(0+offset) );
            stuff.Add( 0+offset );

            return stuff;
        }
        public List<object> first(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(0+offset) );
            stuff.Add( 0+offset );

            return stuff;
        }
        public List<object> start(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(0+offset) );
            stuff.Add( 0+offset );

            return stuff;
        }


        // back
        public List<object> back(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(length-offset-1) );
            stuff.Add( length-offset-1 );

            return stuff;
        }
        public List<object> last(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(length-offset-1) );
            stuff.Add( length-offset-1 );

            return stuff;
        }
        public List<object> end(int offset=0) {
            List<object> stuff = new List<object>(2);

            stuff.Add( get(length-offset-1) );
            stuff.Add( length-offset-1 );

            return stuff;
        }


        // replace
        public Bucket replace(object old, object with) {
            Bucket newContents = new Bucket(contents.ToArray());

            Bucket stuff = newContents.filter( (v, i) => { 
                return v is string || v is char && v.ToString().Contains(old.ToString());
            });

            int at = newContents.indexOf(stuff.get(0));
            string replaced = newContents.get(at).ToString().Replace( old.ToString(), with.ToString() );

            newContents.set( at,  replaced);

            return newContents;
        }

        
        // replaceAll
        public Bucket replaceAll(object old, object with) {
            Bucket newContents = new Bucket(contents.ToArray());

            newContents = newContents.map( (v, i) => {
                return (v is string || v is char && v.ToString().Contains(old.ToString())) ? v.ToString().Replace( old.ToString(), with.ToString()) : v;
            });

            return newContents;
        }


        // append
        public void append(int index, object with) {
            contents.Insert(index, with);
        }
    }
}
