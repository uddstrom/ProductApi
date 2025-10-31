import React from "react";
import ProductConfigForm from "./ProductConfigForm";
import productSchema from "./productSchema.json"; // Import your schema
import { convertJsonSchemaToZod } from "zod-from-json-schema";

const zodSchema = convertJsonSchemaToZod(productSchema);

const App = () => {
    const handleFormSubmit = (formData) => {
        console.log("Form Data:", formData);
    };

    return (
        <div className="container mt-5">
            <ProductConfigForm
                zSchema={zodSchema}
                schema={productSchema}
                onSubmit={handleFormSubmit}
            />
        </div>
    );
};

export default App;
