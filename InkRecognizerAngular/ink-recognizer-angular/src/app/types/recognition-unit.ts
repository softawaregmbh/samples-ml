import { RecognitionAlternative } from './recognition-alternative';
import { BoundingRectangle } from './bounding-rectangle';

export interface RecognitionUnit {
    alternates: RecognitionAlternative[],
    category: string,
    class: string,
    id: number,
    parentId: number,
    recognizedText: string,
    strokeIds: number[],
    confidence: number,
    recognizedObject: string,
    boundingRectangle: BoundingRectangle
}