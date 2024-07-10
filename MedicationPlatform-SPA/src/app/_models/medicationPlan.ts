import { Medication } from './medication';
import { Patient } from './patient';

export interface MedicationPlan {
    id: string;
    intakeIntervals: string;
    startDate: Date;
    endDate: Date;
    patient: Patient;
    medication: Medication[];
}