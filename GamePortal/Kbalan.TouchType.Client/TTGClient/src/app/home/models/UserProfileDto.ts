import { StatisticDto } from './StatisticDto';
import { SettingDto } from './SettingDto';

export interface UserProfileDto {
  Id?: string;
  Username?: string;
  RegistrationDate?: Date;
  Email?: string;
  Statistic?: StatisticDto;
  Setting?: SettingDto;
  Avatar?: string;
  IsBlocked?: boolean;
}
