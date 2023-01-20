export interface IKeyedCollection<T1 extends string | number,T2> {
    Add(key: T1, value: T2);
    AddRange(keys: T1[], values: T2[]);
    ContainsKey(key: T1): boolean;
    Count(): number;
    Item(key: T1): T2;
    Keys(): T1[];
    Remove(key: T1): T2;
    Values(): T2[];
}
