export interface Patient {
    id: string;
    email: string;
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
    gender: string;
    role: string;
    birthDate: Date;
    address: string;
    medicalRecord: string;
    caregiverId: string;
}