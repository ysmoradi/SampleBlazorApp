// You can use ProtectedStorage package of Blazor instead
// You can call localStorage.setItem and localStorage.getItem directly from C#
// following code is for learning purposes only

function getLocalStorageValue(key: string): any {
    return localStorage[key];
}

function setLocalStorageValue(key: string, value?: any): void {
    if (value != null) {
        localStorage[key] = value;
    }
    else {
        localStorage.removeItem(key);
    }
}