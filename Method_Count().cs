/* Взагалі існує як властивість Count так і метод Count(), їх різниця полягає в тому що Count() це метод розширення, представлений
 LINQ, тоді як Count властивість є частиною самої колекції похідної від інтерфейсу ICollection*/

public static int Count<TSource>(this IEnumerable<TSource> source) 
{
    if (source == null) throw Error.ArgumentNull("source");
    ICollection<TSource> collectionoft = source as ICollection<TSource>; 
    if (collectionoft != null) return collectionoft.Count;
    ICollection collection = source as ICollection;
    if (collection != null) return collection.Count;
    
    /* Складність метода полягає у тому що він намагається переобразувати вхідну колекцію в ICollection.
     При успішному перетворенні повертається значення властивості Count.
     У разі невдачі потрібно буде перебрати всю колекцію для обраховування кількості елементів, внизу як раз бачимо як у нас перебираються елементи */  
    
    using (IEnumerator<TSource> e = source.GetEnumerator()) 
    {
        checked 
        {
            while (e.MoveNext()) count++;
        }
    }
    return count;
}

/* Взагалі краще використовувувати властивість count ніж метод count(), якщо все буде успішно і наш IEnumerable імплементує ICollection
   то час виконання алгоритму методу буде О(1) що не буде різнитися з властивістю count, але в крайньому випадку може виконатися за час  О(n)*/ 