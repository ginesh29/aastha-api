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
  public dataSource = new MatTableDataSource<Patient>();

  pageSize = 15;
  skip = 0;
  totalLength = 0;
  pageIndex = 0;
  pageSizeOptions = [15, 30, 45, 60, 75];

  firstnameFilter = new FormControl('');
  middlenameFilter = new FormControl('');
  lastnameFilter = new FormControl('');
  addressFilter = new FormControl('');
  ageFilter = new FormControl('');
  mobileFilter = new FormControl('');
  filterValues = {
    Firstname: '',
    Middlename: '',
    Lastname: '',
    Address: '',
    Age: '',
    Mobile: ''
  };
  // @ViewChild(MatSort) sort: MatSort;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private repoService: RepositoryService) {
  }

  ngOnInit() {
    this.firstnameFilter.valueChanges
      .subscribe(
        name => {
          alert();
          this.filterValues.Firstname = name;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.getAllPatients();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  public getAllPatients = () => {
    this.repoService.getData('api/patients', this.pageIndex, this.pageSize)
      .subscribe(res => {
        this.dataSource.data = res.Result as Patient[];
        this.totalLength = res.Count;
      });
  }

  tableFilter(): (data: any, filter: string) => boolean {
    const filterFunction = function(data, filter): boolean {
      const searchTerms = JSON.parse(filter);
      return data.Firstname.toLowerCase().indexOf(searchTerms.Firstname) !== -1;
        // && data.id.toString().toLowerCase().indexOf(searchTerms.id) !== -1
        // && data.colour.toLowerCase().indexOf(searchTerms.colour) !== -1
        // && data.pet.toLowerCase().indexOf(searchTerms.pet) !== -1;
    };
    return filterFunction;
  }

  public redirectToDetails = (id: string) => {

  }

  public redirectToUpdate = (id: string) => {

  }

  public redirectToDelete = (id: string) => {

  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
    this.dataSource.filterPredicate = this.tableFilter();
    console.log(this.dataSource.filterPredicate);
    this.getAllPatients();
  }
  // public doSort = (value: string) => {
  //   this.getAllPatients();
  // }
  public pageChanged = (event) => {
      this.pageSize = event.pageSize;
      this.pageIndex = event.pageIndex;
      console.log(event);
      this.getAllPatients();
  }
}

