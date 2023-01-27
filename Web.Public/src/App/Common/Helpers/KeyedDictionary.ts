import type { IKeyedCollection } from "./IKeyedCollection";

export class KeyedCollection<T1 extends string | number,T2> implements IKeyedCollection<T1,T2> {
    private items: Record<T1,T2>;

    
    private count: number = 0;
    
    constructor() {
        this.items = <Record<T1,T2>>{};
    }
 
    public ContainsKey(key: T1): boolean {
        return this.items.hasOwnProperty(key);
    }

    public Count(): number {
        return this.count;
    }
 
    public Add(key: T1, value: T2) {
        if(!this.items.hasOwnProperty(key))
             this.count++;
 
        this.items[key] = value;
    }
    public AddRange(keys: T1[], values: T2[]) {
        if(keys.length != values.length)
         return;
        for (let index = 0; index < keys.length; index++) {
            this.Add(keys[index],values[index])
            
        }
    }
 
    public Remove(key: T1): T2 {
        var val = this.items[key];
        delete this.items[key];
        this.count--;
        return val;
    }
 
    public Item(key: T1): T2 {
        return this.items[key];
    }
 
    public Keys(): T1[] {
        var keySet: T1[] = [];
 
        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                keySet.push(prop);
            }
        }
 
        return keySet;
    }
 
    public Values(): T2[] {
        var values: T2[] = [];
 
        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                values.push(this.items[prop]);
            }
        }
 
        return values;
    }
}