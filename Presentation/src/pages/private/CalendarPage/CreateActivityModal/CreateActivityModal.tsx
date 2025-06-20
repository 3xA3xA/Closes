import { useEffect, useState } from "react";
import styles from './CreateActivityModal.module.css'
import { createGroupActivity } from "../../../../api/ActivityService/activityService";
import { useParams } from "react-router-dom";

interface ActivityFormData {
    name: string;
    description: string;
    type: number;
    status: number;
    startAt: Date;
    endAt: Date | null;
    groupId: string;
}

interface CreateActivityModalProps {
    selectedDate: Date | null;
    onCancel: () => void;
    onCreate: (activity: ActivityFormData) => void;
}

export const CreateActivityModal = ({ selectedDate, onCancel, onCreate }: CreateActivityModalProps) => {
    const { groupId } = useParams()
    const [formData, setFormData] = useState<ActivityFormData>({
        name: '',
        description: '',
        type: 0,
        status: 0,
        startAt: selectedDate ? new Date(selectedDate.setHours(12, 0, 0, 0)) : new Date(),
        endAt: selectedDate ? new Date(selectedDate.setHours(13, 0, 0, 0)) : null,
        groupId: groupId ?? ''
    });

    useEffect(() => {
        if (selectedDate) {
            setFormData(prev => ({
                ...prev,
                startAt: new Date(selectedDate.setHours(12, 0, 0, 0)),
                endAt: new Date(selectedDate.setHours(13, 0, 0, 0))
            }));
        }
    }, [selectedDate]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleTimeChange = (e: React.ChangeEvent<HTMLInputElement>, field: 'startAt' | 'endAt') => {
        const time = e.target.value;
        if (!selectedDate || !time) return;
        
        const [hours, minutes] = time.split(':').map(Number);
        const newDate = new Date(selectedDate);
        newDate.setHours(hours, minutes, 0, 0);
        
        setFormData(prev => ({ ...prev, [field]: newDate }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onCreate(formData)
        createGroupActivity({
            ...formData,
            type: parseInt(formData.type)
        })
    };

    if (!selectedDate) return null;

    return (
        <div className={styles.modalOverlay}>
            <div className={styles.modal}>
                <h3>Создать активность</h3>
                <form onSubmit={handleSubmit}>
                    <div className={styles.formGroup}>
                        <label>Название:</label>
                        <input
                            type="text"
                            name="name"
                            value={formData.name}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label>Описание:</label>
                        <textarea
                            name="description"
                            value={formData.description}
                            onChange={handleInputChange}
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label>Тип активности:</label>
                        <select
                            name="type"
                            value={formData.type}
                            onChange={handleInputChange}
                        >
                            <option value={0}>Спорт</option>
                            <option value={1}>Встреча</option>
                            <option value={2}>Обучение</option>
                            <option value={3}>Другое</option>
                        </select>
                    </div>
                    <div className={styles.formGroup}>
                        <label>Дата:</label>
                        <input
                            type="date"
                            value={selectedDate.toISOString().split('T')[0]}
                            disabled
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label>Начало:</label>
                        <input
                            type="time"
                            value={formData.startAt.toTimeString().substring(0, 5)}
                            onChange={(e) => handleTimeChange(e, 'startAt')}
                            required
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label>Окончание:</label>
                        <input
                            type="time"
                            value={formData.endAt?.toTimeString().substring(0, 5) || '13:00'}
                            onChange={(e) => handleTimeChange(e, 'endAt')}
                        />
                    </div>
                    <div className={styles.buttonGroup}>
                        <button type="button" onClick={onCancel}>Отмена</button>
                        <button type="submit">Создать</button>
                    </div>
                </form>
            </div>
        </div>
    );
};