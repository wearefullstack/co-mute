

export type IError<TForm> = Partial<{[key in keyof TForm]: string }>

export function stringExists(str? : string | null){
    if(str){
        return str.length > 0;
    }
    return false;
}