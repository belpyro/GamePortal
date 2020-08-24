import { StatisticDto } from 'src/app/home/models/StatisticDto';



export interface UserStatisticDto {
  Id?: string;
  Username?: string;
  RegistrationDate?: Date;
  Email?: string;
  Statistic?: StatisticDto;
}
