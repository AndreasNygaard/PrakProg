using static System.Math;
public struct vector3d{
        public double x,y,z;
        //constructors
        public vector3d(double a,double b,double c){x=a;y=b;z=c;}
        //operators
        public static vector3d operator*(vector3d v, double c){return new vector3d(c*v.x,c*v.y,c*v.z);}
        public static vector3d operator*(double c, vector3d v){return v*c;}
        public static vector3d operator+(vector3d u, vector3d v){return new vector3d(u.x+v.x,u.y+v.y,u.z+v.z);}
        public static vector3d operator-(vector3d u, vector3d v){return u+(-1*v);}
        //methods
        public static double dot_product(vector3d u, vector3d v){
                double result = u.x*v.x+u.y*v.y+u.z*v.z;
                return result;
        }
        public static vector3d vector_product(vector3d u, vector3d v){
                vector3d result=new vector3d(u.y*v.z-u.z*v.y,u.z*v.x-u.x*v.z,u.x*v.y-u.y*v.x);
                return result;
        }
        public static double magnitude(vector3d v){return Sqrt(Pow(v.x,2)+Pow(v.y,2)+Pow(v.z,2));}
        public  override string ToString(){
                string str=$"({x},{y},{z})";
                return str;
        }
}
