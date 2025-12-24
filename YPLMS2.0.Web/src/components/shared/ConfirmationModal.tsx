import React, { useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import AlertMessageInsideModal from '../../components/shared/AlertMessageInsideModal';

interface modalConfig {
  title: string;
  message: string;
  primaryBtnText: string;
}
interface modalAlertMessageConfig {
  type: "success" | "error" | "info";
  message: string;
  duration?: string;
  autoClose?: boolean;
}
interface ConfirmationModalProps {
  show: boolean;
  onConfirm: () => void;
  onCancel: () => void;
  modalConfig?: modalConfig;
  modalAlertMessageConfig?: modalAlertMessageConfig;
}

const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  show,
  onConfirm,
  onCancel,
  modalConfig = {
    title: '',
    message: '',
    primaryBtnText: ''
  },
  modalAlertMessageConfig = {
    type: '',
    message: '',
    duration: '4000',
    autoClose: true
  }
}) => {
  const finalAlertConfig = {
    type: modalAlertMessageConfig.type,
    message: modalAlertMessageConfig.message,
    duration: modalAlertMessageConfig.duration || '4000',
    autoClose: modalAlertMessageConfig.autoClose ?? true,
  };
  useEffect(() => {
    if (!finalAlertConfig.autoClose || !finalAlertConfig.message) return;
  
    const timer = setTimeout(() => {
      onCancel();
    }, parseInt(finalAlertConfig.duration));
  
    return () => clearTimeout(timer);
  }, [finalAlertConfig]);

  return (
    <Modal 
      className='modal fade yp-modal yp-modal-type-confirmation yp-modal-right-side'
      show={show} 
      onHide={onCancel} 
      backdrop="static" 
      keyboard={false} 
      dialogClassName="modal-dialog modal-sm modal-dialog-scrollable modal-fullscreen-sm-down">
      <Modal.Header closeButton>
        <Modal.Title>
          {modalConfig.title}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        { !finalAlertConfig.message && 
          <div className='yp-confirm-modal-content'>
            {
              modalConfig.primaryBtnText === 'Delete' && (
                <>
                  <div className="yp-confirm-modal-content-icon">
                    <i className="fa fa-trash-alt yp-color-danger"></i>
                  </div>
                </>
              )
            }
            <p>{modalConfig.message}</p>          
            <Button variant={modalConfig.primaryBtnText === 'Delete' ? 'danger' : 'primary'} onClick={onConfirm}>{modalConfig.primaryBtnText}</Button>
            <Button variant="secondary" onClick={onCancel}>Cancel</Button>
          </div>
        }

        { finalAlertConfig.type && finalAlertConfig.message && 
          <AlertMessageInsideModal type={finalAlertConfig.type} message={finalAlertConfig.message} duration={finalAlertConfig.duration} autoClose={finalAlertConfig.autoClose} />
        }
      </Modal.Body>
    </Modal>
  );
};

export default ConfirmationModal;
