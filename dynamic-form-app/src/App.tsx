import React from 'react';
import ProductConfigForm from './ProductConfigForm';
import productSchema from './productSchemaAli.json'; // Import your schema
import { convertJsonSchemaToZod } from 'zod-from-json-schema';
import { z } from 'zod';

const zodSchema = convertJsonSchemaToZod(productSchema as JSONSchema);

type Product = z.infer<typeof zodSchema>;

const p: Product = {
    test: 'abc',
    payoutPeriod: 64,
};

const valRes = zodSchema.safeParse(p);

console.log('JsonSchema', productSchema);
console.log('ZodSchema', zodSchema.shape);
console.log('p', p);
console.log('valRes', valRes);

const App = () => {
    const handleFormSubmit = (formData: HTMLFormElement) => {
        console.log('Form Data:', formData);
    };

    return (
        <div className='container mt-5'>
            <ProductConfigForm zSchema={zodSchema} jSchema={productSchema} onSubmit={handleFormSubmit} />
        </div>
    );
};

export default App;
