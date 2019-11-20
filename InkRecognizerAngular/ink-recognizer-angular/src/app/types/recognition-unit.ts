import { RecognitionAlternative } from './recognition-alternative';

export interface RecognitionUnit {
    alternates: RecognitionAlternative[],
    category: string,
    class: string,
    id: number,
    parentId: number,
    recognizedText: string,
    strokeIds: number[],
    confidence: number,
    recognizedObject: string
}