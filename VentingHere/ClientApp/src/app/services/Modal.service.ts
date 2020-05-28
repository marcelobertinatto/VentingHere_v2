import { Injectable } from '@angular/core';
import { NgbModal, NgbModalOptions, NgbModalRef, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modals: any[] = [];
  closeResult: string;

constructor(private modal: NgbModal) { }

  add(modal: any) {
     this.modals.push(modal);
 }

 remove(id: string) {
     this.modals = this.modals.filter(x => x.id !== id);
 }

 open(content: any, config: NgbModalOptions = {}): NgbModalRef {
     const modalView = this.modal.open(content, config);
     return modalView;
 }

 register(content: any, config: NgbModalOptions = {}): NgbModalRef {
  const modalView = this.modal.open(content, config);
  return modalView;
 }

 close() {
  this.modal.dismissAll();
}

}
