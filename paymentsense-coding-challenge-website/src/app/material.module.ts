import {NgModule} from '@angular/core';

import {
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatPaginatorModule,
    MatDialogModule,
    MatProgressSpinnerModule, 
    MatTableModule,
    MatCardModule
} from '@angular/material'

@NgModule({
    imports: [
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatPaginatorModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        MatTableModule,
        MatCardModule 
    ],
    exports: [
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatPaginatorModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        MatTableModule,
        MatCardModule 
    ]
})

export class MaterialModule {}