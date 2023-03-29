export interface Group {
 name: string;
 connnections: Connection[];
}

export interface Connection {
 connectionId: string;
 username: string;
}