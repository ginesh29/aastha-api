import { RepositoryService } from './../../shared/repository.service';
import { Patient } from './../../interfaces/patient.interface';
import { Component, OnInit, NgModule, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit, AfterViewInit {
  public displayedColumns = ['Firstname', 'Middlename', 'Lastname', 'Address', 'Age', 'Mobile'];
  public dataSource = new MatTableDataSource();

  pageSize = 15;
  skip = 0;
  totalLength = 0;
  pageIndex = 0;
  pageSizeOptions = [15, 30, 45, 60, 75];

  // nameFilter = new FormControl('');
  // filterValues = {
  //   Firstname: '',
  //   Middlename: '',
  //   Lastname: '',
  //   Address: '',
  //   Age: '',
  //   Mobile: ''
  // };
  // @ViewChild(MatSort) sort: MatSort;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private repoService: RepositoryService) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
  }
  ngAfterViewInit(): void {
    this.getAllPatients();
  }

  public getAllPatients = () => {
    this.repoService.getData('api/patients', this.pageIndex, this.pageSize)
      .subscribe(res => {
        this.dataSource.data = res.Result as Patient[];
        this.totalLength = res.Count;
      });
  }

  public redirectToDetails = (id: string) => {

  }

  public redirectToUpdate = (id: string) => {

  }

  public redirectToDelete = (id: string) => {

  }
  // public doFilter = (value: string) => {
  //   this.dataSource.filter = value.trim().toLocaleLowerCase();
  //   this.getAllPatients();
  // }
  // public doSort = (value: string) => {
  //   this.getAllPatients();
  // }
  public pageChanged = (event) => {
      this.pageSize = event.pageSize;
      this.pageIndex = event.pageIndex;
      this.totalLength = event.length;
      this.getAllPatients();
  }
}

