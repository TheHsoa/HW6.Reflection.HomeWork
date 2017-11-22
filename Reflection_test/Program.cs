using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Reflection_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var grelka = (Cat)Activator.CreateInstance(typeof(Cat));
            var zarapka = (Cat)Activator.CreateInstance(typeof(Cat), new object[] { 10 });

            var murzik = new Cat();
            var animalType = typeof(Cat);

            animalType.InvokeMember("_sound", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.SetField, null, null, new object[] { Dog.Voice() });

            Console.WriteLine(Cat.Voice());

            foreach (var attribute in animalType.GetCustomAttributes())
            {
                Console.WriteLine(attribute.ToString());
            }

            Console.WriteLine("---------------------------------------------------");

            foreach (var method in animalType.GetMethods())
            {
                Console.WriteLine(method.ToString());
            }

            Console.WriteLine("---------------------------------------------------");


            var animalsTypes = FindAllDerivedTypes<IAnimal>();

            var animalCollection = new List<IAnimal>();

            foreach (var type in animalsTypes)
            {
                var newAnimal = (IAnimal)Activator.CreateInstance(type);
                SetAllDefaultAttributesForObjectProperties(newAnimal);
                animalCollection.Add((IAnimal)Activator.CreateInstance(type));
            }

            Console.ReadKey();
        }

        public static List<Type> FindAllDerivedTypes<T>()
        {
            var derivedType = typeof(T);
            var assembley = Assembly.GetAssembly(derivedType);

            return assembley.GetTypes().Where(x => x != derivedType && derivedType.IsAssignableFrom(x)).ToList();
        }

        public static void SetAllDefaultAttributesForObjectProperties<T>(T obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                var propertyDefaultValueAttribute = property.GetCustomAttributes(typeof(DefaultValueAttribute)).FirstOrDefault() as DefaultValueAttribute;
                if (propertyDefaultValueAttribute != null)
                {
                    property.SetValue(obj, propertyDefaultValueAttribute.Value);
                    Console.WriteLine($"Type: {obj.GetType().Name}. Property: {property.Name}. DefaultValue: {property.GetValue(obj)}");
                }
            }
        }
    }
}
