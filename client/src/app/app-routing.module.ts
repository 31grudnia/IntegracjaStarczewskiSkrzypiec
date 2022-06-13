import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DaneComponent } from './dane/dane.component';
import { HomeComponent } from './home/home.component';
import { WykresComponent } from './wykres/wykres.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'wykres', component: WykresComponent },
      { path: 'dane', component: DaneComponent },
    ],
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
